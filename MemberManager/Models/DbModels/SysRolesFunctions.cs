using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Models.DbModels
{
    public class SysRolesFunctions : AbstractAppEntity
    {
        public Int64 sysRoleId { get; set; }
        public Int64 sysFunctionsId { get; set; }
    }
}
