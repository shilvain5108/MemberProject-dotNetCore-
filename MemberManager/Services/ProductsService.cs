using MemberManager.Manager;
using MemberManager.Models.DbModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Services
{
    public class ProductsService
    {
        private readonly ProductTypesManager productTypesManager;

        public ProductsService(ProductTypesManager _productTypesManager)
        {
            productTypesManager = _productTypesManager;
        }

        public List<SelectListItem> GetProductSelectListItemWithSelectedProductTypesId(Int64 selectedProductTypesId = 0)
        {
            List<SelectListItem> items = productTypesManager.GetProductSelectListItem();
            if(items != null && items.Count > 0)
            {
                if(selectedProductTypesId > 0)
                {
                    foreach (SelectListItem item in items)
                    {
                        if (item.Value.Equals(selectedProductTypesId.ToString()))
                        {
                            item.Selected = true;
                        }
                    }
                }
            }

            return items;
        }
    }
}
