using MemberManager.Context;
using MemberManager.Models.DbModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Manager
{
    public class MemberDatasManager : AbstractEntityManager<MemberDatas>
    {
        public MemberDatasManager(MemberContext _db,
   IHttpContextAccessor _httpContextAccessor) : base(_db)
        {
        }

        public override IQueryable<MemberDatas> GetEntitiesQ()
        {
            return db.MemberDatas;
        }

        public MemberDatas GetById(Int64 memberId)
        {
            MemberDatas member = db.MemberDatas.Find(memberId);
            return member;
        }
        public MemberDatas GetByAcc(string account)
        {
            MemberDatas member = null;
            if(!string.IsNullOrEmpty(account))
            {
                member = db.MemberDatas.Where(m => account.Equals(m.loginAccount)).ToList().FirstOrDefault();
            }
            return member;
        }
    }
}
