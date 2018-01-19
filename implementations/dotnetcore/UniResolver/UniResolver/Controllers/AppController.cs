using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniResolver.Services;
using UniResolver.ViewModels;

namespace UniResolver.Controllers
{
    public class AppController : Controller
    {
        private readonly IDidResolverService _didResolverService;
        private readonly IStringLocalizer<AppController> _localizer;
        private readonly ILogger<AppController> _logger;

        public AppController(IStringLocalizer<AppController> localizer, ILogger<AppController> logger, IDidResolverService didResolverService)
        {
            _didResolverService = didResolverService;
            _localizer = localizer;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Resolve()
        {
            ViewBag.Title = _localizer["Resolve"];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Resolve(ResolverViewModel model)
        {
            if(ModelState.IsValid)
            {
                // lookup DID
                var ddo = await _didResolverService.ResolveDidAsync(model.Did);
                ViewBag.UserMessage = "DID " + _localizer["resolved"] + " : " + JsonConvert.SerializeObject(ddo);
                ModelState.Clear();
            }
            else
            {
                // show errors
            }
            return View();
        }

    }
}
