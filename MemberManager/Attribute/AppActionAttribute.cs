using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Attribute
{
    [AttributeUsage(AttributeTargets.All)]

    public class AppActionAttribute : System.Attribute
    {
        private string Name;

        public AppActionAttribute(string _name)
        {
            this.Name = _name;
        }

        public string GetName()
        {
            return this.Name;
        }
    }
}
