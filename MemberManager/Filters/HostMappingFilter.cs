using MemberManager.Context;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Filters
{
    public class HostMappingFilter : ActionFilterAttribute
    {
        private readonly MemberContext db;

        public HostMappingFilter(MemberContext _db)
        {
            db = _db;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            await base.OnActionExecutionAsync(filterContext, next);
        }
    }
}
