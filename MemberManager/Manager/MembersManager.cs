using MemberManager.Context;
using MemberManager.Models.DbModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Manager
{
    public class MembersManager : AbstractEntityManager<Members>
    {
        public MembersManager(MemberContext _db,
   IHttpContextAccessor _httpContextAccessor) : base(_db)
        {
        }

        public override IQueryable<Members> GetEntitiesQ()
        {
            return db.Members.Where(m => !m.removed);
        }

        public Members GetById(Int64 memberId)
        {
            Members member = db.Members.Find(memberId);
            return member;
        }
        public Members GetByAcc(string account)
        {
            Members member = null;
            if (!string.IsNullOrEmpty(account))
            {
                member = db.Members.Where(m => account.Equals(m.loginAccount)).ToList().FirstOrDefault();
            }
            return member;
        }
    }
}
