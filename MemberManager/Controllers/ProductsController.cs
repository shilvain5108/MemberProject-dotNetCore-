using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemberManager.Context;
using MemberManager.Extensions;
using MemberManager.Manager;
using MemberManager.Models.DbModels;
using MemberManager.Models.ViewSearchModel;
using MemberManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MemberManager.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ISession session;
        private readonly ILogger<HomeController> logger;
        private readonly IHttpContextAccessor httpContextAccessor;

        private readonly ProductsManager productsManager;
        private readonly ProductTypesManager productTypesManager;
        private readonly ProductsService productsService;

        public ProductsController(
          ProductsManager _productsManager,
          ProductTypesManager _productTypesManager,
          ProductsService _productsService,

          ILogger<HomeController> _logger,
          IHttpContextAccessor _httpContextAccessor)
        {
            logger = _logger;
            httpContextAccessor = _httpContextAccessor;
            session = _httpContextAccessor.HttpContext.Session;

            productsManager = _productsManager;
            productTypesManager = _productTypesManager;
            productsService = _productsService;
        }

        public async Task<IActionResult> Index(ProductSearchModel parameters = null)
        {
            ProductsManager.Criteria criteria = new ProductsManager.Criteria();
            if (parameters != null)
            {
                criteria.name = parameters.productName;
                criteria.productTypesId = parameters.productTypesId;
            }

            List<Products> productses = await productsManager.ExcuteQuery(criteria);
            productsManager.PrepareData(productses);

            UserContext userContext = session.GetObjectFromJson<UserContext>(UserContext.SESSION_NAME.ToString());

            ViewData["searchParameters"] = parameters;

            //取得產品類別的清單
            List<ProductTypes> productTypeses = productTypesManager.GetEntitiesQ().ToList();
            List<SelectListItem> items = productTypesManager.GetProductSelectListItem();



            ViewData["productTypeses"] = items;

            return View(productses);
        }

        public IActionResult Edit(Int64? id)
        {
            Products products = new Products();
            if (id != null && id > 0)
            {
                List<Products> productses = productsManager.GetByIds(new List<Int64>() { Convert.ToInt64(id) }).ToList();
                products = productsManager.GetById(Convert.ToInt64(id));
            }

            UserContext userContext = session.GetObjectFromJson<UserContext>(UserContext.SESSION_NAME.ToString());
            ViewData["userContext"] = userContext;

            List<ProductTypes> productTypeses = productTypesManager.GetEntitiesQ().ToList();

            Int64 productTypesId = 0;
            if (products != null && products.productTypeId > 0)
                productTypesId = products.productTypeId;

            ViewData["productTypeses"] = productsService.GetProductSelectListItemWithSelectedProductTypesId(productTypesId);

            return View(products);
        }

        public string Save(Products products)//    string id,string name
        {
            Dictionary<String, Object> valueObject = new Dictionary<string, object>();

            Products editProducts = null;
            if (products.id > 0)
            {
                editProducts = productsManager.GetById(products.id);
                if (editProducts != null)
                {
                    editProducts.name = products.name;
                    editProducts.productTypeId = products.productTypeId;
                    editProducts.price = products.price;
                    editProducts.sort = products.sort;
                }
            }
            else
                editProducts = products;

            try
            {
                if (editProducts != null)
                {
                    productsManager.Save(editProducts);

                    valueObject.Add("success", true);
                    valueObject.Add("message", "儲存成功");
                }
                else
                    throw new Exception("資料錯誤!");
            }
            catch (Exception ex)
            {
                valueObject.Add("success", false);
                valueObject.Add("message", "儲存失敗，錯誤訊息:" + ex.Message);
            }

            return JsonConvert.SerializeObject(valueObject);
        }
    }
}
