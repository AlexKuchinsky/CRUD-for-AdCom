using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AdmissionCommittee.Domain.Entities
{
    public enum EducationLevel
    {
        [Description("Preschool education")]
        PreschoolEducation,
        [Description("General secondary education")]
        GeneralSecondaryEducation,
        [Description("Special education")]
        SpecialEducation,
        [Description("Vocational education")]
        VocationalEducation,
        [Description("Secondary special education")]
        SecondarySpecialEducation,
        [Description("Higher education")]
        HigherEducation
    }

    class Description : Attribute
    {
        public string Text;
        public Description(string text)
        {
            Text = text;
        }
    }

    public static class StringEducationLevel
    {
        public static string Description(this EducationLevel en)
        {
            Type type = en.GetType();      
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(Description), false);
                if (attrs != null && attrs.Length > 0)
                    return ((Description)attrs[0]).Text;
            }
            return en.ToString();
        }
    }
}
