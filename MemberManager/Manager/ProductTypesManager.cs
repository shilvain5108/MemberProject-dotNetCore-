using MemberManager.Context;
using MemberManager.Models.DbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<List<ProductTypes>> ExcuteQuery(Criteria criteria)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder sql = new StringBuilder();
            sql.Append(" Select * From [" + ProductTypes.TABLE_NAME + "] as pt ");
            sql.Append(" Where 1 = 1 ");

            if(!string.IsNullOrWhiteSpace(criteria.productTypeName))
            {
                sql.Append(" And pt.name like @name ");
                parameters.Add(new SqlParameter("name","%" + criteria.productTypeName + "%"));
            }

            sql.Append(" And pt.removed = 0 ");

            return await db.ProductTypes.FromSqlRaw(sql.ToString(), parameters.ToArray()).ToListAsync();
        }

        public List<SelectListItem> GetProductSelectListItem()
        {
            List<ProductTypes> productTypeses = GetEntitiesQ().ToList();

            List<SelectListItem> items =
                new List<SelectListItem>() { new SelectListItem("請選擇", "0") };
            if (productTypeses != null && productTypeses.Count > 0)
            {
                items.AddRange(productTypeses.Select(m => new SelectListItem(m.name, m.id.ToString())).ToList());
            }

            return items;
        }

        public class Criteria
        {
            public string productTypeName { get; set; }
        }
    }
}
