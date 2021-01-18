using MemberManager.Context;
using MemberManager.Models.DbModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Manager
{
    public class SysRolesFunctionsManager : AbstractEntityManager<SysRolesFunctions>
    {
        public SysRolesFunctionsManager(MemberContext _db,
          IHttpContextAccessor _httpContextAccessor) : base(_db)
        {
        }

        public override IQueryable<SysRolesFunctions> GetEntitiesQ()
        {
            return db.SysRolesFunctions.Where(m => !m.removed);
        }
    }
}
