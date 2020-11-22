using MemberManager.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Context
{
    /// <summary>
    /// 裝載跟登入User相關的所有物件
    /// </summary>
    public class UserContext
    {
        public const string SESSION_NAME = "UserContext";
        public MemberDatas memberData;
    }
}
