using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemberManager.Context;
using MemberManager.Extensions;
using MemberManager.Manager;
using MemberManager.Models.DbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public ProductsController(
          ILogger<HomeController> _logger,
          IHttpContextAccessor _httpContextAccessor,
          ProductsManager _productsManager)
        {
            logger = _logger;
            httpContextAccessor = _httpContextAccessor;
            session = _httpContextAccessor.HttpContext.Session;

            productsManager = _productsManager;
        }

        public IActionResult Index()
        {
            List<Products> productses = productsManager.GetEntitiesQ().ToList();

            UserContext userContext = session.GetObjectFromJson<UserContext>(UserContext.SESSION_NAME.ToString());

            return View(productses);
        }

        public IActionResult Edit(Int64? productsId)
        {
            Products products = new Products();
            if (productsId != null && productsId > 0)
            {
                List<Products> productses = productsManager.GetByIds(new List<Int64>() { Convert.ToInt64(productsId) }).ToList();
                products = productsManager.GetById(Convert.ToInt64(productsId));
            }

            UserContext userContext = session.GetObjectFromJson<UserContext>(UserContext.SESSION_NAME.ToString());
            ViewData["userContext"] = userContext;

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
