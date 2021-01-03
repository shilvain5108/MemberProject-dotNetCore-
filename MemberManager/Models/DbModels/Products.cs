using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Models.DbModels
{
    public class Products : AbstractAppEntity
    {
        public Int64 productTypeId { get; set; }

        public string name { get; set; }

        public int price { get; set; }

        public int sort { get; set; }
    }
}
