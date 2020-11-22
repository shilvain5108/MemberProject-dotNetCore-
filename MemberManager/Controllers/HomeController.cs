using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MemberManager.Models;
using MemberManager.Models.DbModels;
using MemberManager.Manager;
using Microsoft.AspNetCore.Http;
using MemberManager.Context;
using MemberManager.Extensions;

namespace MemberManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISession session;
        private readonly ILogger<HomeController> _logger;
        private readonly MemberDatasManager memberDatasManager;

        public HomeController(
            MemberDatasManager _memberDatasManager,
            ILogger<HomeController> logger, 
            IHttpContextAccessor _httpContextAccessor)
        {
            memberDatasManager = _memberDatasManager;
            _logger = logger;
            session = _httpContextAccessor.HttpContext.Session;
        }

        public IActionResult Index()
        {
            List<MemberDatas> members = memberDatasManager.GetEntitiesQ().ToList();
            MemberDatas member = memberDatasManager.GetById(1);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult UserLogin(string userAcc,string userPwd)
        {
            MemberDatas member = memberDatasManager.GetByAcc(userAcc);
            if (member != null)
            {
                CreateUserSession(member);
                UserContext userContext = session.GetObjectFromJson<UserContext>(UserContext.SESSION_NAME.ToString());

                UserContext userContext2 = session.GetObjectFromJson<UserContext>(UserContext.SESSION_NAME.ToString());

                return View("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Error",new { errorCode = "404", errorMessage = "幹...好" });
            }
        }

        private void CreateUserSession(MemberDatas memberData)
        {
            UserContext userContext = session.GetObjectFromJson<UserContext>(UserContext.SESSION_NAME.ToString());
            userContext.memberData = memberData;

            session.SetObjectAsJson(UserContext.SESSION_NAME.ToString(), userContext);
        }
    }
}
