using MemberManager.Models;
using MemberManager.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Context
{
    public class UserContext
    {
        public const string SESSION_NAME = "UserContext";

        public Members user;

        public List<SiteRole> roles;

        public List<SysFunctions> sysFunctions;//顯示上方的功能清單
    }
}
