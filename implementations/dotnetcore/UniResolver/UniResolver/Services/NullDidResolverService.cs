using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniResolver.Services
{
    public class NullDidResolverService : IDidResolverService
    {
        private readonly ILogger<NullDidResolverService> _logger;
        public NullDidResolverService(ILogger<NullDidResolverService> logger)
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
            // log the request
            _logger.LogInformation($"Async resolve Did request for {did}");
            DecentralizedIdentifierDescriptionObject ddo = new DecentralizedIdentifierDescriptionObject();
            return ddo;
        }

    }
}
