using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

[AttributeUsage(AttributeTargets.All)]
public class ColumnAttribute : Attribute
{
    private string displayName;
    private string customValue;

    public ColumnAttribute(string _displayName)
    {
        this.displayName = _displayName;
    }
    public ColumnAttribute(string _displayName, string _customValue)
    {
        this.displayName = _displayName;
        this.customValue = _customValue;
    }
    public static string GetDisplayName(object enm)
    {
        if (enm != null)
        {
            MemberInfo[] mi = enm.GetType().GetMember(enm.ToString());
            if (mi != null && mi.Length > 0)
            {
                ColumnAttribute attr = Attribute.GetCustomAttribute(mi[0],
                    typeof(ColumnAttribute)) as ColumnAttribute;
                if (attr != null)
                {
                    return attr.displayName;
                }
            }
        }
        return null;
    }

    public static string GetCustomValue(object enm)
    {
        if (enm != null)
        {
            MemberInfo[] mi = enm.GetType().GetMember(enm.ToString());
            if (mi != null && mi.Length > 0)
            {
                ColumnAttribute attr = Attribute.GetCustomAttribute(mi[0],
                    typeof(ColumnAttribute)) as ColumnAttribute;
                if (attr != null)
                {
                    return attr.customValue;
                }
            }
        }
        return null;
    }

    public string GetDisplayName()
    {
        return this.displayName;
    }

    public string GetCustomValue()
    {
        return this.customValue;
    }
}

