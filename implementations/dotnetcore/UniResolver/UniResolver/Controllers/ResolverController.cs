using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UniResolver.Services;

namespace UniResolver.Controllers
{
    [Route("api/[Controller]")]
    public class ResolverController : Controller
    {
        private readonly IDidResolverService _didResolverService;
        private readonly ILogger<ResolverController> _logger;

        public ResolverController(IDidResolverService didResolverService, ILogger<ResolverController> logger)
        {
            _didResolverService = didResolverService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var ddo = await _didResolverService.ResolveDidAsync(id);
                return Ok(ddo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to resolve DID: {ex}");
                return BadRequest("Failed to resolve DID"); 
            }
        }
    }
}