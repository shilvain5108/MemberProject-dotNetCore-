using MemberManager.Context;
using MemberManager.Models.DbModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Manager
{
    public class OrderStatusManager : AbstractAppEntityManager<OrderStatus>
    {
        public OrderStatusManager(MemberContext _db,
IHttpContextAccessor _httpContextAccessor) : base(_db)
        {
        }

        public override IQueryable<OrderStatus> GetEntitiesQ()
        {
            return db.OrderStatus.Where(m => !m.removed);
        }

        public override OrderStatus GetById(Int64 id)
        {
            return db.OrderStatus.Where(m => !m.removed && m.id == id).FirstOrDefault();
        }
    }
}
