using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MemberManager.Models
{
    [ColumnAttribute("是否")]
    public enum Yes_No
    {
        Yes = 0,
        No = 1
    }
}
