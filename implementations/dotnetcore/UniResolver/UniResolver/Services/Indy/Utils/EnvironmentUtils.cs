using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UniResolver.Services.Indy.Utils
{
    static class EnvironmentUtils
    {
        public static string GetTestPoolIP()
        {
            var testPoolIp = Environment.GetEnvironmentVariable("TEST_POOL_IP");
            return testPoolIp != null ? testPoolIp : "127.0.0.1";
        }

        public static string GetTmpPath()
        {
            return Path.Combine(Path.GetTempPath(), "indy");
        }

        public static string GetTmpPath(string filename)
        {
            return GetTmpPath() + filename;
        }
    }
}
