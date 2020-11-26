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
using MemberManager.Attribute;

namespace MemberManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISession session;
        private readonly ILogger<HomeController> logger;
        private readonly MembersManager membersManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public HomeController(
            MembersManager _membersManager,
            ILogger<HomeController> _logger, 
            IHttpContextAccessor _httpContextAccessor)
        {
            membersManager = _membersManager;
            logger = _logger;
            httpContextAccessor = _httpContextAccessor;
            session = _httpContextAccessor.HttpContext.Session;
        }

        public IActionResult Index()
        {
            UserContext userContext = session.GetObjectFromJson<UserContext>(UserContext.SESSION_NAME.ToString());

           
            Members member = userContext.member;
            if(member != null)
            {
                ViewData["memberName"] = member.name;
            }

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
            Members member = null;
            string errMsg = "";

            //防呆
            if (string.IsNullOrWhiteSpace(userAcc))
                errMsg += "請輸入密碼\n";

            if (string.IsNullOrWhiteSpace(userPwd))
                errMsg += "請輸入密碼\n";

            if(string.IsNullOrWhiteSpace(errMsg))
            {
                member = membersManager.GetByAcc(userAcc);
                if (member != null)
                {
                    if (string.IsNullOrWhiteSpace(member.loginPwd))
                        errMsg += "請通知管理員協助修改登入密碼\n";
                    else if(!member.loginPwd.Equals(userPwd))
                        errMsg += "密碼錯誤\n";
                }
                else
                    errMsg += "找不到帳號啦幹\n";
            }

            if (string.IsNullOrWhiteSpace(errMsg) && member != null)
            {
                CreateUserSession(member);

                return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Index", "Error", new { errorCode = "404", errorMessage = errMsg });
        }

        private void CreateUserSession(Members members)
        {
            UserContext userContext = session.GetObjectFromJson<UserContext>(UserContext.SESSION_NAME.ToString());
            if (userContext == null) userContext = new UserContext();

            userContext.member = members;

            session.SetObjectAsJson(UserContext.SESSION_NAME.ToString(), userContext);

            userContext = session.GetObjectFromJson<UserContext>(UserContext.SESSION_NAME.ToString());

            Members tempMembers = userContext.member;
        }
    }
}
