using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Models.DbModels
{
    public class OrderDetails : AbstractAppEntity
    {
        public int productId { get; set; }

        public int statusId { get; set; }
    }
}
