using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Attribute
{
    [AttributeUsage(AttributeTargets.All)]
    public class IgnoreUserLoginFilterAttribute : System.Attribute
    {
    }
}
