using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Models.DbModels
{
    public class Products : AbstractAppEntity
    {
        public const string TABLE_NAME = "products";

        public Int64 productTypeId { get; set; }

        public string name { get; set; }

        public int price { get; set; }

        public int sort { get; set; }

        [NotMapped]
        public ProductTypes productTypes { get; set; }
    }
}
