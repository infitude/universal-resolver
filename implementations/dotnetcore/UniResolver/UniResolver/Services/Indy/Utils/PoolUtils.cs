using Hyperledger.Indy.PoolApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UniResolver.Services.Indy.Utils
{
    static class PoolUtils
    {
        public static string CreateGenesisTxnFile(string filename)
        {
            var file = EnvironmentUtils.GetTmpPath(filename);
            var testPoolIp = EnvironmentUtils.GetTestPoolIP();

            var liveTxns = new string[]{
                "{\"data\":{\"alias\":\"ev1\",\"client_ip\":\"54.207.36.81\",\"client_port\":9702,\"node_ip\":\"18.231.96.215\",\"node_port\":9701,\"services\":[\"VALIDATOR\"]},\"dest\":\"GWgp6huggos5HrzHVDy5xeBkYHxPvrRZzjPNAyJAqpjA\",\"identifier\":\"J4N1K1SEB8uY2muwmecY5q\",\"txnId\":\"b0c82a3ade3497964cb8034be915da179459287823d92b5717e6d642784c50e6\",\"type\":\"0\"}", 
                "{\"data\":{\"alias\":\"zaValidator\",\"client_ip\":\"154.0.164.39\",\"client_port\":9702,\"node_ip\":\"154.0.164.39\",\"node_port\":9701,\"services\":[\"VALIDATOR\"]},\"dest\":\"BnubzSjE3dDVakR77yuJAuDdNajBdsh71ZtWePKhZTWe\",\"identifier\":\"UoFyxT8BAqotbkhiehxHCn\",\"txnId\":\"d5f775f65e44af60ff69cfbcf4f081cd31a218bf16a941d949339dadd55024d0\",\"type\":\"0\"}", 
                "{\"data\":{\"alias\":\"danube\",\"client_ip\":\"128.130.204.35\",\"client_port\":9722,\"node_ip\":\"128.130.204.35\",\"node_port\":9721,\"services\":[\"VALIDATOR\"]},\"dest\":\"476kwEjDj5rxH5ZcmTtgnWqDbAnYJAGGMgX7Sq183VED\",\"identifier\":\"BrYDA5NubejDVHkCYBbpY5\",\"txnId\":\"ebf340b317c044d970fcd0ca018d8903726fa70c8d8854752cd65e29d443686c\",\"type\":\"0\"}", 
                "{\"data\":{\"alias\":\"royal_sovrin\",\"client_ip\":\"35.167.133.255\",\"client_port\":9702,\"node_ip\":\"35.167.133.255\",\"node_port\":9701,\"services\":[\"VALIDATOR\"]},\"dest\":\"Et6M1U7zXQksf7QM6Y61TtmXF1JU23nsHCwcp1M9S8Ly\",\"identifier\":\"4ohadAwtb2kfqvXynfmfbq\",\"txnId\":\"24d391604c62e0e142ea51c6527481ae114722102e27f7878144d405d40df88d\",\"type\":\"0\"}", 
                "{\"data\":{\"alias\":\"digitalbazaar\",\"client_ip\":\"34.226.105.29\",\"client_port\":9701,\"node_ip\":\"34.226.105.29\",\"node_port\":9700,\"services\":[\"VALIDATOR\"]},\"dest\":\"D9oXgXC3b6ms3bXxrUu6KqR65TGhmC1eu7SUUanPoF71\",\"identifier\":\"rckdVhnC5R5WvdtC83NQp\",\"txnId\":\"56e1af48ef806615659304b1e5cf3ebf87050ad48e6310c5e8a8d9332ac5c0d8\",\"type\":\"0\"}", 
                "{\"data\":{\"alias\":\"OASFCU\",\"client_ip\":\"38.70.17.248\",\"client_port\":9702,\"node_ip\":\"38.70.17.248\",\"node_port\":9701,\"services\":[\"VALIDATOR\"]},\"dest\":\"8gM8NHpq2cE13rJYF33iDroEGiyU6wWLiU1jd2J4jSBz\",\"identifier\":\"BFAeui85mkcuNeQQhZfqQY\",\"txnId\":\"825aeaa33bc238449ec9bd58374b2b747a0b4859c5418da0ad201e928c3049ad\",\"type\":\"0\"}", 
                "{\"data\":{\"alias\":\"BIGAWSUSEAST1-001\",\"client_ip\":\"34.224.255.108\",\"client_port\":9796,\"node_ip\":\"34.224.255.108\",\"node_port\":9769,\"services\":[\"VALIDATOR\"]},\"dest\":\"HMJedzRbFkkuijvijASW2HZvQ93ooEVprxvNhqhCJUti\",\"identifier\":\"L851TgZcjr6xqh4w6vYa34\",\"txnId\":\"40fceb5fea4dbcadbd270be6d5752980e89692151baf77a6bb64c8ade42ac148\",\"type\":\"0\"}", 
                "{\"data\":{\"alias\":\"DustStorm\",\"client_ip\":\"207.224.246.57\",\"client_port\":9712,\"node_ip\":\"207.224.246.57\",\"node_port\":9711,\"services\":[\"VALIDATOR\"]},\"dest\":\"8gGDjbrn6wdq6CEjwoVStjQCEj3r7FCxKrA5d3qqXxjm\",\"identifier\":\"FjuHvTjq76Pr9kdZiDadqq\",\"txnId\":\"6d1ee3eb2057b8435333b23f271ab5c255a598193090452e9767f1edf1b4c72b\",\"type\":\"0\"}", 
                "{\"data\":{\"alias\":\"prosovitor\",\"client_ip\":\"138.68.240.143\",\"client_port\":9711,\"node_ip\":\"138.68.240.143\",\"node_port\":9710,\"services\":[\"VALIDATOR\"]},\"dest\":\"C8W35r9D2eubcrnAjyb4F3PC3vWQS1BHDg7UvDkvdV6Q\",\"identifier\":\"Y1ENo59jsXYvTeP378hKWG\",\"txnId\":\"15f22de8c95ef194f6448cfc03e93aeef199b9b1b7075c5ea13cfef71985bd83\",\"type\":\"0\"}",
                "{\"data\":{\"alias\":\"iRespond\",\"client_ip\":\"52.187.10.28\",\"client_port\":9702,\"node_ip\":\"52.187.10.28\",\"node_port\":9701,\"services\":[\"VALIDATOR\"]},\"dest\":\"3SD8yyJsK7iKYdesQjwuYbBGCPSs1Y9kYJizdwp2Q1zp\",\"identifier\":\"JdJi97RRDH7Bx7khr1znAq\",\"txnId\":\"b65ce086b631ed75722a4e1f28fc9cf6119b8bc695bbb77b7bdff53cfe0fc2e2\",\"type\":\"0\"}"
             };


            var sandboxTxns = new string[]{
                string.Format("{{\"data\":{{\"alias\":\"Node1\",\"blskey\":\"4N8aUNHSgjQVgkpm8nhNEfDf6txHznoYREg9kirmJrkivgL4oSEimFF6nsQ6M41QvhM2Z33nves5vfSn9n1UwNFJBYtWVnHYMATn76vLuL3zU88KyeAYcHfsih3He6UHcXDxcaecHVz6jhCYz1P2UZn2bDVruL5wXpehgBfBaLKm3Ba\",\"client_ip\":\"{0}\",\"client_port\":9702,\"node_ip\":\"{0}\",\"node_port\":9701,\"services\":[\"VALIDATOR\"]}},\"dest\":\"Gw6pDLhcBcoQesN72qfotTgFa7cbuqZpkX3Xo6pLhPhv\",\"identifier\":\"Th7MpTaRZVRYnPiabds81Y\",\"txnId\":\"fea82e10e894419fe2bea7d96296a6d46f50f93f9eeda954ec461b2ed2950b62\",\"type\":\"0\"}}", testPoolIp),
                string.Format("{{\"data\":{{\"alias\":\"Node2\",\"blskey\":\"37rAPpXVoxzKhz7d9gkUe52XuXryuLXoM6P6LbWDB7LSbG62Lsb33sfG7zqS8TK1MXwuCHj1FKNzVpsnafmqLG1vXN88rt38mNFs9TENzm4QHdBzsvCuoBnPH7rpYYDo9DZNJePaDvRvqJKByCabubJz3XXKbEeshzpz4Ma5QYpJqjk\",\"client_ip\":\"{0}\",\"client_port\":9704,\"node_ip\":\"{0}\",\"node_port\":9703,\"services\":[\"VALIDATOR\"]}},\"dest\":\"8ECVSk179mjsjKRLWiQtssMLgp6EPhWXtaYyStWPSGAb\",\"identifier\":\"EbP4aYNeTHL6q385GuVpRV\",\"txnId\":\"1ac8aece2a18ced660fef8694b61aac3af08ba875ce3026a160acbc3a3af35fc\",\"type\":\"0\"}}", testPoolIp),
                string.Format("{{\"data\":{{\"alias\":\"Node3\",\"blskey\":\"3WFpdbg7C5cnLYZwFZevJqhubkFALBfCBBok15GdrKMUhUjGsk3jV6QKj6MZgEubF7oqCafxNdkm7eswgA4sdKTRc82tLGzZBd6vNqU8dupzup6uYUf32KTHTPQbuUM8Yk4QFXjEf2Usu2TJcNkdgpyeUSX42u5LqdDDpNSWUK5deC5\",\"client_ip\":\"{0}\",\"client_port\":9706,\"node_ip\":\"{0}\",\"node_port\":9705,\"services\":[\"VALIDATOR\"]}},\"dest\":\"DKVxG2fXXTU8yT5N7hGEbXB3dfdAnYv1JczDUHpmDxya\",\"identifier\":\"4cU41vWW82ArfxJxHkzXPG\",\"txnId\":\"7e9f355dffa78ed24668f0e0e369fd8c224076571c51e2ea8be5f26479edebe4\",\"type\":\"0\"}}", testPoolIp),
                string.Format("{{\"data\":{{\"alias\":\"Node4\",\"blskey\":\"2zN3bHM1m4rLz54MJHYSwvqzPchYp8jkHswveCLAEJVcX6Mm1wHQD1SkPYMzUDTZvWvhuE6VNAkK3KxVeEmsanSmvjVkReDeBEMxeDaayjcZjFGPydyey1qxBHmTvAnBKoPydvuTAqx5f7YNNRAdeLmUi99gERUU7TD8KfAa6MpQ9bw\",\"client_ip\":\"{0}\",\"client_port\":9708,\"node_ip\":\"{0}\",\"node_port\":9707,\"services\":[\"VALIDATOR\"]}},\"dest\":\"4PS3EDQ3dW1tci1Bp6543CfuuebjFrg36kLAUcskGfaA\",\"identifier\":\"TWwCRQRZ2ZHMJFn9TzLp7W\",\"txnId\":\"aa5e817d7cc626170eca175822029339a444eb0ee8f0bd20d3b0b76e566fb008\",\"type\":\"0\"}}", testPoolIp)
             };

            var defaultTxns = new string[]{
            "{\"data\":{\"alias\":\"Node1\",\"client_ip\":\"212.232.28.103\",\"client_port\":9702,\"node_ip\":\"212.232.28.103\",\"node_port\":9701,\"services\":[\"VALIDATOR\"]},\"dest\":\"Gw6pDLhcBcoQesN72qfotTgFa7cbuqZpkX3Xo6pLhPhv\",\"identifier\":\"Th7MpTaRZVRYnPiabds81Y\",\"txnId\":\"fea82e10e894419fe2bea7d96296a6d46f50f93f9eeda954ec461b2ed2950b62\",\"type\":\"0\"}",
            "{\"data\":{\"alias\":\"Node2\",\"client_ip\":\"212.232.28.103\",\"client_port\":9704,\"node_ip\":\"212.232.28.103\",\"node_port\":9703,\"services\":[\"VALIDATOR\"]},\"dest\":\"8ECVSk179mjsjKRLWiQtssMLgp6EPhWXtaYyStWPSGAb\",\"identifier\":\"EbP4aYNeTHL6q385GuVpRV\",\"txnId\":\"1ac8aece2a18ced660fef8694b61aac3af08ba875ce3026a160acbc3a3af35fc\",\"type\":\"0\"}",
            "{\"data\":{\"alias\":\"Node3\",\"client_ip\":\"212.232.28.103\",\"client_port\":9706,\"node_ip\":\"212.232.28.103\",\"node_port\":9705,\"services\":[\"VALIDATOR\"]},\"dest\":\"DKVxG2fXXTU8yT5N7hGEbXB3dfdAnYv1JczDUHpmDxya\",\"identifier\":\"4cU41vWW82ArfxJxHkzXPG\",\"txnId\":\"7e9f355dffa78ed24668f0e0e369fd8c224076571c51e2ea8be5f26479edebe4\",\"type\":\"0\"}",
            "{\"data\":{\"alias\":\"Node4\",\"client_ip\":\"212.232.28.103\",\"client_port\":9708,\"node_ip\":\"212.232.28.103\",\"node_port\":9707,\"services\":[\"VALIDATOR\"]},\"dest\":\"4PS3EDQ3dW1tci1Bp6543CfuuebjFrg36kLAUcskGfaA\",\"identifier\":\"TWwCRQRZ2ZHMJFn9TzLp7W\",\"txnId\":\"aa5e817d7cc626170eca175822029339a444eb0ee8f0bd20d3b0b76e566fb008\",\"type\":\"0\"}",
             };

            Directory.CreateDirectory(Path.GetDirectoryName(file));
            var stream = new StreamWriter(file);

            foreach (var defaultTxn in defaultTxns)
            {
                stream.WriteLine(defaultTxn);
            }

            stream.Close();

            return file;
        }

        public static async Task CreatePoolLedgerConfig(string poolName)
        {
            var genesisTxnFile = CreateGenesisTxnFile("temp.txn");
            var path = Path.GetFullPath(genesisTxnFile).Replace('\\', '/');
            var createPoolLedgerConfig = string.Format("{{\"genesis_txn\":\"{0}\"}}", path);

            try
            {
                await Pool.CreatePoolLedgerConfigAsync(poolName, createPoolLedgerConfig);
            }
            catch (PoolLedgerConfigExistsException)
            {
                //Swallow expected exception if it happens.
            }
        }

        public static async Task DeletePoolLedgerConfigAsync(string name)
        {
            try
            {
                await Pool.DeletePoolLedgerConfigAsync(name);
            }
            catch (IOException) //TODO: This should be a more specific error when implemented
            {
                //Swallow expected exception if it happens.                
            }
        }
    }
}
