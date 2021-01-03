using MemberManager.Context;
using MemberManager.Models.DbModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Manager
{
    public class ProductsManager : AbstractEntityManager<Products>
    {
        public ProductsManager(MemberContext _db,
IHttpContextAccessor _httpContextAccessor) : base(_db)
        {
        }

        public override IQueryable<Products> GetEntitiesQ()
        {
            return db.Products.Where(m => !m.removed).OrderBy(m => m.sort);
        }

        public Products GetById(Int64 id)
        {
            return db.Products.Where(m => !m.removed && m.id == id).FirstOrDefault();
        }
    }
}
