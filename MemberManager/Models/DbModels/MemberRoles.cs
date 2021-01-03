using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Models.DbModels
{
    public class MemberRoles : AbstractAppEntity
    {
        public Int64 memberId { get; set; }

        public Int64 sysRolesId { get; set; }
    }
}
