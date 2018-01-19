using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Hyperledger.Indy.LedgerApi;
using UniResolver.Services.Indy.Utils;
using Hyperledger.Indy.PoolApi;
using Hyperledger.Indy.WalletApi;
using Hyperledger.Indy.SignusApi;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace UniResolver.Services.Indy
{
    public class IndyDidResolverService : IDidResolverService
    {
        private static readonly string DID_SOV_PATTERN = "^did:sov:(\\S*)$";
        private static readonly string DEFAULT_POOL_CONFIG_NAME = "11347-04";
        private static readonly string DEFAULT_WALLET_NAME = "default";

        private static readonly string DDO_CURVE = "ed25519";
        private static readonly List<string> DDO_OWNER_TYPES = new List<string> { "CryptographicKey", "EdDsaSAPublicKey" };


        private readonly ILogger<IndyDidResolverService> _logger;

        public IndyDidResolverService(ILogger<IndyDidResolverService> logger)
        {
            _logger = logger;
        }

        public DecentralizedIdentifierDescriptionObject ResolveDid(string did)
        {
            // log the request
            _logger.LogInformation($"Resolve Did request for {did}");
            return new DecentralizedIdentifierDescriptionObject();
        }

        public async Task<DecentralizedIdentifierDescriptionObject> ResolveDidAsync(string did)
        {
            _logger.LogInformation($"Async Resolve Did request for {did}");
            DecentralizedIdentifierDescriptionObject ddo = new DecentralizedIdentifierDescriptionObject();

            // check passed in did
            MatchCollection matches = Regex.Matches(did, DID_SOV_PATTERN, RegexOptions.IgnoreCase);
            string targetDid = matches[0].Groups[1].Value;

            // create and open pool
            await PoolUtils.CreatePoolLedgerConfig(DEFAULT_POOL_CONFIG_NAME);

            // create and open my wallet
            await WalletUtils.CreateWalleatAsync(DEFAULT_POOL_CONFIG_NAME, DEFAULT_WALLET_NAME, "default", null, null);

            // Open pool and wallet in using statements to ensure they are closed when finished.
            using (var pool = await Pool.OpenPoolLedgerAsync(DEFAULT_POOL_CONFIG_NAME, "{}"))
            using (var wallet = await Wallet.OpenWalletAsync(DEFAULT_WALLET_NAME, null, null))
            {
                // Create submitter Did
                var createSubmitterDidResult = await Signus.CreateAndStoreMyDidAsync(wallet, "{}");
                var submitterDid = createSubmitterDidResult.Did;

                // Get Nym 
                var getNymRequest = await Ledger.BuildGetNymRequestAsync(submitterDid, targetDid);
                var getNymReponse = await Ledger.SignAndSubmitRequestAsync(pool, wallet, submitterDid, getNymRequest);

                // parse out attributes from json, get the data element (which contains an embedded json object)
                string verkey = "";
                try
                {
                    JObject nym = JObject.Parse(getNymReponse);
                    string nymDataJson = (string)nym.SelectToken("result.data");
                    JObject nymData = JObject.Parse(nymDataJson);
                    verkey = (string)nymData.SelectToken("verkey");
                }
                catch (Exception ex)
                {
                    //throw;
                }

                // Get attr
                var getAttrRequest = await Ledger.BuildGetAttribRequestAsync(submitterDid, targetDid, "endpoint");
                var getAttrResponse = await Ledger.SignAndSubmitRequestAsync(pool, wallet, submitterDid, getAttrRequest);

                // DDO id
                string id = did;

                // DDO owners
                Owner owner = new Owner { id = did, type = DDO_OWNER_TYPES, curve = DDO_CURVE, publicKeyBase64 = verkey, publicKeyHex = null };
                List<Owner> owners = new List<Owner> { owner };
                
                // DDO controls
                List<Control> controls = new List<Control>();

                // DDO services
                string endpoint = "";
                try
                {
                    JObject attr = JObject.Parse(getAttrResponse);
                    string attrDataJson = (string)attr.SelectToken("result.data");
                    JObject attrData = JObject.Parse(attrDataJson);
                    endpoint = (string)attrData.SelectToken("endpoint");
                }
                catch (Exception ex)
                {
                    //throw;
                }

                Service service = new Service();
                //TODO: sort out services 

                // create DDO
                ddo = new DecentralizedIdentifierDescriptionObject { id = id, owner = owners, control = controls, service = service };
            }
            return ddo;
        }
    }
}
