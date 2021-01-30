using MemberManager.Context;
using MemberManager.Models.DbModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Manager
{
    public class OrderDetailsManager : AbstractAppEntityManager<OrderDetails>
    {
        public OrderDetailsManager(MemberContext _db,
           IHttpContextAccessor _httpContextAccessor) : base(_db)
        {
        }

        public override IQueryable<OrderDetails> GetEntitiesQ()
        {
            return db.OrderDetails.Where(m => !m.removed);
        }

        public override OrderDetails GetById(Int64 id)
        {
            return db.OrderDetails.Where(m => !m.removed && m.id == id).FirstOrDefault();
        }
    }
}
