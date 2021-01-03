using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemberManager.Attribute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MemberManager.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(
            ILogger<ErrorController> _logger,
            IHttpContextAccessor _httpContextAccessor)
        {
            logger = _logger;
        }

        [IgnoreUserLoginFilter]
        public IActionResult Index(string errorCode, string errorMessage)
        {
            ViewData["errorCode"] = errorCode;
            ViewData["errorMessage"] = errorMessage;
            return View();
        }

    }
}
