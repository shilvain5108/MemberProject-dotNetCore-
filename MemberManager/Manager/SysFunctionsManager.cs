using MemberManager.Context;
using MemberManager.Models.DbModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Manager
{
    public class SysFunctionsManager : AbstractEntityManager<SysFunctions>
    {
        public SysFunctionsManager(MemberContext _db,
          IHttpContextAccessor _httpContextAccessor) : base(_db)
        {
        }

        public override IQueryable<SysFunctions> GetEntitiesQ()
        {
            return db.SysFunctions.Where(m => !m.removed);
        }
    }
}
