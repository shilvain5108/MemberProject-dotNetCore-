using MemberManager.Context;
using MemberManager.Models.DbModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Manager
{
    public class SendTypesManager : AbstractAppEntityManager<SendTypes>
    {
        public SendTypesManager(MemberContext _db,
IHttpContextAccessor _httpContextAccessor) : base(_db)
        {
        }

        public override IQueryable<SendTypes> GetEntitiesQ()
        {
            return db.SendTypes.Where(m => !m.removed);
        }

        public override SendTypes GetById(Int64 id)
        {
            return db.SendTypes.Where(m => !m.removed && m.id == id).FirstOrDefault();
        }
    }
}
