using MemberManager.Context;
using MemberManager.Models.DbModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Manager
{
    public class MemberRolesManager : AbstractEntityManager<MemberRoles>
    {
        public MemberRolesManager(MemberContext _db,
          IHttpContextAccessor _httpContextAccessor) : base(_db)
        {
        }

        public override IQueryable<MemberRoles> GetEntitiesQ()
        {
            return db.MemberRoles.Where(m => !m.removed);
        }
    }
}
