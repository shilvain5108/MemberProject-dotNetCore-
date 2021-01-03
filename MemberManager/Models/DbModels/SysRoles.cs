using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Models.DbModels
{
    public class SysRoles : AbstractAppEntity
    {
        public string name
        {
            get { return siteRole.ToString(); }
            set
            {
                SiteRole res; if (!Enum.TryParse((string)value, out res)) throw new ApplicationException(string.Format("Can't convert '{0}' to type [{1}]", value, typeof(SiteRole)));
                siteRole = res;
            }
        }

        [NotMapped]
        public SiteRole siteRole { get; set; }
    }
}
