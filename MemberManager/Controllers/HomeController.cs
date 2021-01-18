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
        private readonly ProductsManager productsManager;
        private readonly SysRolesManager sysRolesManager;
        private readonly MemberRolesManager memberRolesManager;
        private readonly SysFunctionsManager sysFunctionsManager;
        private readonly SysRolesFunctionsManager sysRolesFunctionsManager;

        public HomeController(
            ProductsManager _productsManager,
            MembersManager _membersManager,
            SysRolesManager _sysRolesManager,
            MemberRolesManager _memberRolesManager,
            SysFunctionsManager _sysFunctionsManager,
            SysRolesFunctionsManager _sysRolesFunctionsManager,

            IHttpContextAccessor _httpContextAccessor,
            ILogger<HomeController> _logger)
        {
            membersManager = _membersManager;
            productsManager = _productsManager;
            sysRolesManager = _sysRolesManager;
            memberRolesManager = _memberRolesManager;
            sysFunctionsManager = _sysFunctionsManager;
            sysRolesFunctionsManager = _sysRolesFunctionsManager;

            logger = _logger;
            session = _httpContextAccessor.HttpContext.Session;
        }

        public IActionResult Index()
        {
            UserContext userContext = session.GetObjectFromJson<UserContext>(UserContext.SESSION_NAME.ToString());
            ViewData["userContext"] = userContext;

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

        [IgnoreUserLoginFilter]
        public IActionResult Login()
        {
            ClearLoginInformation();
            return View();
        }

        [IgnoreUserLoginFilter]
        public IActionResult UserLogin(string userAcc, string userPwd)
        {
            Members member = null;
            string errMsg = "";

            //防呆
            if (string.IsNullOrWhiteSpace(userAcc))
                errMsg += "請輸入密碼\n";

            if (string.IsNullOrWhiteSpace(userPwd))
                errMsg += "請輸入密碼\n";

            if (string.IsNullOrWhiteSpace(errMsg))
            {
                member = membersManager.GetByAcc(userAcc);
                if (member != null)
                {
                    if (string.IsNullOrWhiteSpace(member.loginPwd))
                        errMsg += "請通知管理員協助修改登入密碼\n";
                    else if (!member.loginPwd.Equals(userPwd))
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

        public IActionResult Products()
        {
            List<Products> productses = productsManager.GetEntitiesQ().ToList();
            return View(productses);
        }

        private void CreateUserSession(Members members)
        {
            UserContext userContext = session.GetObjectFromJson<UserContext>(UserContext.SESSION_NAME.ToString());
            if (userContext == null) userContext = new UserContext();

            //網站權限
            if (members != null && members.id > 0)
            {
                //準備登入者的權限清單以及可用的功能清單
                List<MemberRoles> memberRoleses
                    = memberRolesManager.GetEntitiesQ().Where(m => m.memberId == members.id).ToList();
                if (memberRoleses != null && memberRoleses.Count > 0)
                {
                    List<Int64> sysRolesIds = memberRoleses.Select(m => m.sysRolesId).ToList();
                    if (sysRolesIds != null && sysRolesIds.Count > 0)
                    {
                        List<Int64> sysFunctionIds = 
                            sysRolesFunctionsManager.GetEntitiesQ()
                            .Where(m => sysRolesIds.Contains(m.sysRoleId))
                            .Select(m => m.sysFunctionsId).ToList();
                        if(sysFunctionIds != null && sysFunctionIds.Count > 0)
                        {
                            List<SysFunctions> sysFunctions = 
                                sysFunctionsManager.GetEntitiesQ()
                                .Where(m => sysFunctionIds.Contains(m.id)).ToList();

                            userContext.sysFunctions = sysFunctions;
                        }

                        List<SysRoles> sysRoleses = sysRolesManager.GetByIds(sysRolesIds).ToList();
                        userContext.roles = sysRoleses.Select(m => m.siteRole).ToList();
                    }
                }

                userContext.user = members;

                session.SetObjectAsJson(UserContext.SESSION_NAME.ToString(), userContext);
            }
        }

        private void ClearLoginInformation()
        {
            session.SetObjectAsJson(UserContext.SESSION_NAME.ToString(), null);
        }
    }
}
