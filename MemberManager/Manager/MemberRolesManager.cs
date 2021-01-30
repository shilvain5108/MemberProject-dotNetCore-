using MemberManager.Context;
using MemberManager.Models.DbModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Manager
{
    public class MemberRolesManager : AbstractAppEntityManager<MemberRoles>
    {
        public MemberRolesManager(MemberContext _db,
          IHttpContextAccessor _httpContextAccessor) : base(_db)
        {
        }

        public override IQueryable<MemberRoles> GetEntitiesQ()
        {
            return db.MemberRoles.Where(m => !m.removed);
        }

        public override MemberRoles GetById(Int64 id)
        {
            return db.MemberRoles.Where(m => !m.removed && m.id == id).FirstOrDefault();
        }
    }
}
