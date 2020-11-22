using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Models.DbModels
{
    public class MemberDatas : AbstractEntity
    {
        public string name { get; set; }

        public string mobileNumber { get; set; }

        public DateTime? birthday { get; set; }

        public string loginAccount { get; set; }

        public string loginPwd { get; set; }
    }
}
