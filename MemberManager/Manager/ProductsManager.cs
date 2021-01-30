using MemberManager.Context;
using MemberManager.Models.DbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberManager.Manager
{
    public class ProductsManager : AbstractAppEntityManager<Products>
    {
        private readonly ProductTypesManager productTypesManager;

        public ProductsManager(MemberContext _db,
            ProductTypesManager _productTypesManager,
IHttpContextAccessor _httpContextAccessor) : base(_db)
        {
            productTypesManager = _productTypesManager;
        }

        public override IQueryable<Products> GetEntitiesQ()
        {
            return db.Products.Where(m => !m.removed).OrderBy(m => m.sort);
        }

        public override Products GetById(Int64 id)
        {
            return db.Products.Where(m => !m.removed && m.id == id).FirstOrDefault();
        }

        public async Task<List<Products>> ExcuteQuery(Criteria criteria)
        {
            StringBuilder sql = new StringBuilder();
            List<SqlParameter> parameters = new List<SqlParameter>();

            sql.Append(" Select p.* From [" + Products.TABLE_NAME + "] As p ");
            sql.Append(" Where 1 = 1 ");

            if (!string.IsNullOrWhiteSpace(criteria.name))
            {
                sql.Append(" And p.[name] like @name ");
                parameters.Add(new SqlParameter("name", "%" + criteria.name + "%"));
            }

            if (criteria.productTypesId > 0)
            {
                sql.Append(" And p.[productTypeId] = @productTypeId ");
                parameters.Add(new SqlParameter("productTypeId", criteria.productTypesId));
            }

            sql.Append(" And removed = 0 Order By p.productTypeId,p.sort ");

            return await db.Products.FromSqlRaw(sql.ToString(), parameters.ToArray()).ToListAsync();
        }

        public void PrepareData(Products products)
        {
            PrepareData(new List<Products>() { products });
        }

        public void PrepareData(List<Products> productses)
        {
            if (productses != null && productses.Count > 0)
            {
                Dictionary<Int64, ProductTypes> productTypesGroup = productTypesManager.GetEntitiesQ().ToDictionary(m => m.id, m => m);

                foreach (Products products in productses)
                {
                    //準備產品類別的物件
                    if (productTypesGroup.ContainsKey(products.productTypeId))
                    {
                        products.productTypes = productTypesGroup[products.productTypeId];
                    }
                }
            }
        }

        public class Criteria
        {
            public string name { get; set; }

            public Int64 productTypesId { get; set; }
        }
    }
}
