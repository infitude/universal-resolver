using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading.Tasks;
using UniResolver.Services.Indy.Utils;


namespace UnitTestUniResolver
{
    [TestClass]
    public class UnitTestIndy
    {
        [TestMethod]
        public async Task TestIndyServiceGenesisFile()
        {
            var genesisTxnFile = UniResolver.Services.Indy.Utils.PoolUtils.CreateGenesisTxnFile("test.txn");
            var path = Path.GetFullPath(genesisTxnFile).Replace('\\', '/');
        }

        [TestMethod]
        public async Task TestIndyServiceResolveDid()
        {
            // create a 'real' logger
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<UniResolver.Services.Indy.IndyDidResolverService>();

            UniResolver.Services.Indy.IndyDidResolverService _resolver = new UniResolver.Services.Indy.IndyDidResolverService(logger);
            await _resolver.ResolveDidAsync("did:sov:WRfXPg8dantKVubE3HX8pw");
        }

    }
}
