using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Models.DbModels
{
    public class ProductTypes : AbstractAppEntity
    {
        public const string TABLE_NAME = "productTypes";

        public string name { get; set; }

        public int sort { get; set; }
    }
}
