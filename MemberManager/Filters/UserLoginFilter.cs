using MemberManager.Attribute;
using MemberManager.Context;
using MemberManager.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Filters
{
    public class UserLoginFilter : ActionFilterAttribute
    {
        private readonly ILogger<UserLoginFilter> logger;
        private bool isIgnore = false;//action 不檢查是否有登入的flag

        public UserLoginFilter(ILogger<UserLoginFilter> _logger)
        {
            logger = _logger;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            UserContext userContext = filterContext.HttpContext.Session.GetObjectFromJson<UserContext>(UserContext.SESSION_NAME.ToString());

            var descriptor = (ControllerActionDescriptor)filterContext.ActionDescriptor;
            var attributes = descriptor.MethodInfo.CustomAttributes;
            if (attributes.Any(a => a.AttributeType == typeof(IgnoreUserLoginFilterAttribute))) isIgnore = true;

            if ((userContext == null || userContext.user == null) &&
                !isIgnore)
            {
                //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                //{
                //    action = "Login",
                //    controller = "Home"
                //}));

                filterContext.HttpContext.Response.Redirect("Home/Login");
            }
            else
            {
               // if ((userContext != null && userContext.user != null)) userId = userContext.user.id;
                Controller controller = ((Controller)filterContext.Controller);
                ControllerBase controllerBase = ((ControllerBase)filterContext.Controller);
                HttpContext httpContext = filterContext.HttpContext;
                string loginIp = httpContext.Connection.RemoteIpAddress.ToString();
                string host = httpContext.Request.Host.Value;
                //string areaName = descriptor.RouteValues["area"] == null ? "" : filterContext.ActionDescriptor.RouteValues["area"].ToString();
                string controllerName = descriptor.ControllerName;
                string actionName = descriptor.ActionName;
                string queryString = httpContext.Request.QueryString.Value;

                await next();
                //action執行後

                // ViewData["sysLoginLoggerLoginStatus"]：登入頁面寫入sysLoginLogger.loginStatus的資訊
                // ViewData["sysLoginLoggerRemark"]：登入頁面寫入sysLoginLogger.remark的資訊
                //if (controller.ViewData["sysLoginLoggerRemark"] != null && controller.ViewData["sysLoginLoggerLoginStatus"] != null)
                //{
                //    userContext = filterContext.HttpContext.Session.GetObjectFromJson<UserContext>(UserContext.SESSION_NAME.ToString());
                //    if (!isIgnore && (userContext != null && userContext.user != null))
                //    {
                //        string accessUserId = userContext.user.userId;                        
                //        string loginStatus = controller.ViewData["sysLoginLoggerLoginStatus"].ToString();
                //        string remark = controller.ViewData["sysLoginLoggerRemark"].ToString();
                //        string controllerAttributesName = "";
                //        string actionAttributesName = "";
                //        var controllerCustomAttributes = controllerBase.ControllerContext.ActionDescriptor.ControllerTypeInfo.GetCustomAttributes(typeof(AppControllerAttribute), inherit: true);
                //        var actionAttributes = controllerBase.ControllerContext.ActionDescriptor.MethodInfo.GetCustomAttributes(typeof(AppActionAttribute), inherit: true);
                //        if (controllerCustomAttributes != null && controllerCustomAttributes.Length > 0) controllerAttributesName = ((AppControllerAttribute)controllerCustomAttributes[0]).GetName();
                //        if (actionAttributes != null && actionAttributes.Length > 0) actionAttributesName = ((AppActionAttribute)actionAttributes[0]).GetName();

                //        logger.LogInformation(host + "/" + areaName + "/" + controllerName + "/" + actionName + queryString + " == " + controllerAttributesName + ":" + actionAttributesName + "使用者:" + userId + "來源IP:" + loginIp);
                //        SysLoginLogger sysLoginLogger = new SysLoginLogger()
                //        {
                //            accessUserId = accessUserId,
                //            loginIp = loginIp,
                //            loginStatus = loginStatus,
                //            remark = remark,
                //        };
                //        await sysLoginLoggerManager.SaveAsync(sysLoginLogger);                        
                //    }
                //}
            }
        }
    }
}
