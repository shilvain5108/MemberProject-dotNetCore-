using MemberManager.Context;
using MemberManager.Models.DbModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Manager
{
    public class ProductTypesManager : AbstractEntityManager<ProductTypes>
    {
        public ProductTypesManager(MemberContext _db,
IHttpContextAccessor _httpContextAccessor) : base(_db)
        {
        }

        public override IQueryable<ProductTypes> GetEntitiesQ()
        {
            return db.ProductTypes.Where(m => !m.removed);
        }
    }
}
