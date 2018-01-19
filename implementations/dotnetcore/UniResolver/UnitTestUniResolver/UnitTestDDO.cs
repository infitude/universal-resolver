using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using UniResolver.Services;

namespace UnitTestUniResolver
{
    [TestClass]
    public class UnitTestDDO
    {
        [TestMethod]
        public void TestDDOSerialization()
        {
            
            StringBuilder ddoJson = new StringBuilder();
            ddoJson.Append("    { ");
            ddoJson.Append("	\"context\": \"https://example.org/did/v1\",   ");
            ddoJson.Append("	\"id\": \"did:sov:21tDAKCERh95uGgKbJNHYp\",     ");
            ddoJson.Append("    	\"owner\": [{   ");
            ddoJson.Append("		\"id\": \"did:sov:21tDAKCERh95uGgKbJNHYp#key-1\",   ");
            ddoJson.Append("		\"type\": [\"CryptographicKey\", \"EdDsaPublicKey\"],   ");
            ddoJson.Append("		\"curve\": \"ed25519\",   ");
            ddoJson.Append("		\"expires\": \"2017-02-08T16:02:20Z\",   ");
            ddoJson.Append("		\"publicKeyBase64\": \"lji9qTtkCydxtez/bt1zdLxVMMbz4SzWvlqgOBmURoM=\"   ");
            ddoJson.Append("	}, {   ");
            ddoJson.Append("		\"id\": \"did:sov:21tDAKCERh95uGgKbJNHYp#key-2\",   ");
            ddoJson.Append("		\"type\": [\"CryptographicKey\", \"RsaPublicKey\"],   ");
            ddoJson.Append("		\"expires\": \"2017-03-22T00:00:00Z\",   ");
            ddoJson.Append("		\"publicKeyPem\": \"----BEGIN PUBLIC KEY-----\r\nMIIBOgIBAAJBAKkbSUT9/Q2uBfGRau6/XJyZhcF5abo7b37I5hr3EmwGykdzyk8GSyJK3TOrjyl0sdJsGbFmgQaRyV\r\n-----END PUBLIC KEY-----\"   ");
            ddoJson.Append("	}],   ");
            ddoJson.Append("	\"control\": [{   ");
            ddoJson.Append("		\"type\": \"OrControl\",   ");
            ddoJson.Append("		\"signer\": [   ");
            ddoJson.Append("			\"did:sov:21tDAKCERh95uGgKbJNHYp\",   ");
            ddoJson.Append("			\"did:sov:8uQhQMGzWxR8vw5P3UWH1j\"   ");
            ddoJson.Append("		]   ");
            ddoJson.Append("	}],   ");
            ddoJson.Append("	\"service\": {   ");
            ddoJson.Append("		\"openid\": \"https://openid.example.com/456\",   ");
            ddoJson.Append("		\"xdi\": \"https://xdi.example.com/123\"   ");
            ddoJson.Append("	},   ");
            ddoJson.Append("	\"created\": \"2002-10-10T17:00:00Z\",   ");
            ddoJson.Append("	\"updated\": \"2016-10-17T02:41:00Z\",   ");
            ddoJson.Append("	\"signature\": {   ");
            ddoJson.Append("		\"type\": \"RsaSignature2016\",   ");
            ddoJson.Append("		\"created\": \"2016-02-08T16:02:20Z\",   ");
            ddoJson.Append("		\"creator\": \"did:sov:8uQhQMGzWxR8vw5P3UWH1j#key/1\",   ");
            ddoJson.Append("		\"signatureValue\": \"IOmA4R7TfhkYTYW87z640O3GYFldw0yqie9Wl1kZ5OBYNAKOwG5uOsPRK8/2C4STOWF+83cMcbZ3CBMq2/gi25s=\"   ");
            ddoJson.Append("	}   ");
            ddoJson.Append("}   ");

            DecentralizedIdentifierDescriptionObject ddo = JsonConvert.DeserializeObject<DecentralizedIdentifierDescriptionObject>(ddoJson.ToString());
            string serialisedDdo = JsonConvert.SerializeObject(ddo,Formatting.None,new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            string expectedDdo = "{\"context\":\"https://example.org/did/v1\",\"id\":\"did:sov:21tDAKCERh95uGgKbJNHYp\",\"owner\":[{\"id\":\"did:sov:21tDAKCERh95uGgKbJNHYp#key-1\",\"type\":[\"CryptographicKey\",\"EdDsaPublicKey\"],\"curve\":\"ed25519\",\"expires\":\"2017-02-08T16:02:20Z\",\"publicKeyBase64\":\"lji9qTtkCydxtez/bt1zdLxVMMbz4SzWvlqgOBmURoM=\"},{\"id\":\"did:sov:21tDAKCERh95uGgKbJNHYp#key-2\",\"type\":[\"CryptographicKey\",\"RsaPublicKey\"],\"expires\":\"2017-03-22T00:00:00Z\",\"publicKeyPem\":\"----BEGIN PUBLIC KEY-----\\r\\nMIIBOgIBAAJBAKkbSUT9/Q2uBfGRau6/XJyZhcF5abo7b37I5hr3EmwGykdzyk8GSyJK3TOrjyl0sdJsGbFmgQaRyV\\r\\n-----END PUBLIC KEY-----\"}],\"control\":[{\"type\":\"OrControl\",\"signer\":[\"did:sov:21tDAKCERh95uGgKbJNHYp\",\"did:sov:8uQhQMGzWxR8vw5P3UWH1j\"]}],\"service\":{\"openid\":\"https://openid.example.com/456\",\"xdi\":\"https://xdi.example.com/123\"},\"created\":\"2002-10-10T17:00:00Z\",\"updated\":\"2016-10-17T02:41:00Z\",\"signature\":{\"type\":\"RsaSignature2016\",\"created\":\"2016-02-08T16:02:20Z\",\"creator\":\"did:sov:8uQhQMGzWxR8vw5P3UWH1j#key/1\",\"signatureValue\":\"IOmA4R7TfhkYTYW87z640O3GYFldw0yqie9Wl1kZ5OBYNAKOwG5uOsPRK8/2C4STOWF+83cMcbZ3CBMq2/gi25s=\"}}";
            Assert.AreEqual(expectedDdo, serialisedDdo);
        }
    }
}
