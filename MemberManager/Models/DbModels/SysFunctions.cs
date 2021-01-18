using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Models.DbModels
{
    public class SysFunctions : AbstractAppEntity
    {
        public Int64 parentId { get; set; }
        public string displayName { get; set; }
        public string controllerName { get; set; }
        public string actionName { get; set; }
        public int sort { get; set; }
    }
}
