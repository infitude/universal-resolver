using Hyperledger.Indy.WalletApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UniResolver.Services.Indy.Utils
{
    static class WalletUtils
    {
        public static async Task CreateWalleatAsync(string poolName, string name, string type, string config, string credentials)
        {
            try
            {
                await Wallet.CreateWalletAsync(poolName, name, type, config, credentials);
            }
            catch (WalletExistsException)
            {
                //Swallow expected exception if it happens.
            }
        }

        public static async Task DeleteWalletAsync(string name, string credentials)
        {
            try
            {
                await Wallet.DeleteWalletAsync(name, credentials);
            }
            catch (IOException) //TODO: This should be a more specific error when implemented
            {
                //Swallow expected exception if it happens.

            }
        }

    }
}
