using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AppControllerAttribute : System.Attribute
    {
        private string Name;

        public AppControllerAttribute(string _name)
        {
            this.Name = _name;
        }

        public string GetName()
        {
            return this.Name;
        }
    }
}
