using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Timesheet.Micro.Models.Domain.Model
{
    [Flags]
    public enum RoleType
    {
        [Description("Bruker")]
        User = 1,
        //Staffer = 2,
        //KeyAccountManager = 4,
        //ProjectManager = 8,
        //Invoicer = 16,
        //HumanResourcesManager = 32 ,
        //ReportAnalyst = 64 ,
        [Description("Administrator")]
        Administrator = 128
    }

    public static class RoleTypeExtensions
    {
        public static string GetText(this RoleType roleType)
        {
            Type type = roleType.GetType();

            MemberInfo[] memInfo = type.GetMember(roleType.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return roleType.ToString();
        } 
         
    }
}
