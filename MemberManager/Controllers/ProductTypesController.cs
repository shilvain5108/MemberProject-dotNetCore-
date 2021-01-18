using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MemberManager.Models.ViewSearchModel;
using MemberManager.Models.DbModels;
using MemberManager.Manager;
using Microsoft.AspNetCore.Http;

namespace MemberManager.Controllers
{
    public class ProductTypesController : Controller
    {
        private readonly ISession session;
        private readonly ProductTypesManager productTypesManager;

        public ProductTypesController(
            ProductTypesManager _productTypesManager,

            IHttpContextAccessor _httpContextAccessor)
        {
            productTypesManager = _productTypesManager;

            session = _httpContextAccessor.HttpContext.Session;
        }

        public async Task<IActionResult> Index(ProductTypesSearchModel parameters = null)
        {
            List<ProductTypes> productTypes = null;
            if(parameters != null)
            {
                ProductTypesManager.Criteria criteria = new ProductTypesManager.Criteria();
                criteria.productTypeName = parameters.productTypeName;
                productTypes = await productTypesManager.ExcuteQuery(criteria);
            }
            else
            {
                productTypes = new List<ProductTypes>();
            }
            
            ViewData["searchParameters"] = parameters;
            return View(productTypes);
        }
    }
}
