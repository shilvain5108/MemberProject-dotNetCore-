using MemberManager.Context;
using MemberManager.Models.DbModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Manager
{
    public class SysRolesManager : AbstractEntityManager<SysRoles>
    {
        public SysRolesManager(MemberContext _db,
            IHttpContextAccessor _httpContextAccessor) : base(_db)
        {
        }

        public override IQueryable<SysRoles> GetEntitiesQ()
        {
            return db.SysRoles.Where(m => !m.removed);
        }
    }
}
