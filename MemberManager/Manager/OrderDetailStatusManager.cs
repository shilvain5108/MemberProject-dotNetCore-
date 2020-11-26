using MemberManager.Context;
using MemberManager.Models.DbModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Manager
{
    public class OrderDetailStatusManager : AbstractEntityManager<OrderDetailStatus>
    {
        public OrderDetailStatusManager(MemberContext _db,
IHttpContextAccessor _httpContextAccessor) : base(_db)
        {
        }

        public override IQueryable<OrderDetailStatus> GetEntitiesQ()
        {
            return db.OrderDetailStatus.Where(m => !m.removed);
        }
    }
}
