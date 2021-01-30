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
using X.PagedList;//IPagedList

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
            UserContext userContext = session.GetObjectFromJson<UserContext>(UserContext.SESSION_NAME.ToString());

            ProductsManager.Criteria criteria = new ProductsManager.Criteria();
            if (parameters != null)
            {
                criteria.name = parameters.productName;
                criteria.productTypesId = parameters.productTypesId;
            }

            List<Products> productses = await productsManager.ExcuteQuery(criteria);
            productsManager.PrepareData(productses);

            ViewData["searchParameters"] = parameters;

            //取得產品類別的清單
            List<ProductTypes> productTypeses = productTypesManager.GetEntitiesQ().ToList();
            List<SelectListItem> items = productTypesManager.GetProductSelectListItem();

            ViewData["productTypeses"] = items;

            return View(productses);
        }

        public IActionResult Edit(Int64 id = 0)
        {
            Products products = null;
            if (id > 0)
                products = productsManager.GetById(Convert.ToInt64(id));
            else
                products = new Products();

            Int64 productTypesId = 0;
            if (products != null && products.productTypeId > 0)
                productTypesId = products.productTypeId;

            ViewData["productTypeses"] = productsService.GetProductSelectListItemWithSelectedProductTypesId(productTypesId);

            return View(products);
        }

        public async Task<string> Save(Products products)//    string id,string name
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
                    string result = await Verification(editProducts);
                    if (string.IsNullOrWhiteSpace(result))
                    {
                        productsManager.Save(editProducts);

                        valueObject.Add("success", true);
                        valueObject.Add("message", "儲存成功");
                    }
                    else
                        throw new Exception(result);
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

        public string Removed(Int64 id)
        {
            Dictionary<String, Object> valueObject = new Dictionary<string, object>();

            try
            {
                productsManager.Removed(id);

                valueObject.Add("success", true);
                valueObject.Add("message", "儲存成功");
            }
            catch (Exception ex)
            {
                valueObject.Add("success", false);
                valueObject.Add("message", "儲存失敗，錯誤訊息:" + ex.Message);
            }

            return JsonConvert.SerializeObject(valueObject);
        }

        //確認名稱不可與現有的產品名稱重複
        public async Task<string> Verification(Products products)
        {
            string result = "";

            ProductsManager.Criteria criteria = new ProductsManager.Criteria();
            criteria.name = products.name;
            criteria.productTypesId = products.productTypeId;

            List<Products> tempProductses = await productsManager.ExcuteQuery(criteria);
            if (tempProductses != null && tempProductses.Count > 0)
            {
                Products tempProducts = tempProductses.FirstOrDefault();
                if ((products.id <= 0 && tempProductses.Count >= 1) ||
                    (products.id > 0 && tempProductses.Count > 1) ||
                    (tempProductses.Count == 1 && tempProducts.id != products.id))
                {
                    result = "產品名稱重複";
                }
            }

            return result;
        }
    }
}
