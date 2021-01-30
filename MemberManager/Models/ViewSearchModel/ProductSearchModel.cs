using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Models.ViewSearchModel
{
    public class ProductSearchModel
    {
        public const string ViewTitle = "產品列表";

        public int? pageNumber { get; set; }
        public int? maxPageNumber { get; set; }

        public string productName { get; set; }

        public Int64 productTypesId { get; set; }
    }
}
