using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Models.DbModels
{
    public class Orders : AbstractAppEntity
    {
        public Int64 memberDatasId { get; set; }

        public int orderStatusId { get; set; }

        public int sendTypeId { get; set; }

        public string address { get; set; }

        public string phone { get; set; }

        public string memo { get; set; }

        public DateTime createDate { get; set; }
    }
}
