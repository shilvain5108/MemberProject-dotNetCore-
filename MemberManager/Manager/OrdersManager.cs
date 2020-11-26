using MemberManager.Context;
using MemberManager.Models.DbModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Manager
{
    public class OrdersManager : AbstractEntityManager<Orders>
    {
        public OrdersManager(MemberContext _db,
 IHttpContextAccessor _httpContextAccessor) : base(_db)
        {
        }

        public override IQueryable<Orders> GetEntitiesQ()
        {
            return db.Orders.Where(m => !m.removed);
        }
    }
}
