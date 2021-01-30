using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Models.ViewSearchModel
{
    public class SendTypesSearchModel
    {
        public const string ViewTitle = "貨品狀態";

        public int? pageNumber { get; set; }
        public int? maxPageNumber { get; set; }

        public string sendTypesName { get; set; }
    }
}
