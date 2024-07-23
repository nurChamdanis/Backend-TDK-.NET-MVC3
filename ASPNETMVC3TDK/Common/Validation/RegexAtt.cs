using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Toyota.Common.Validation
{
    public class RegexAtt : ValidationAttribute
    {


        public string displayName;
        public string regex;

        public RegexAtt(string regex)
        {
            this.regex = regex;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var attributes = validationContext.ObjectType.GetProperty(validationContext.MemberName).GetCustomAttributes(typeof(DisplayNameAttribute), true);
            if (attributes != null)
                this.displayName = (attributes[0] as DisplayNameAttribute).DisplayName;
            else
                this.displayName = validationContext.DisplayName;

            ErrorMessage = String.Format(Constant.MSG_FORMAT, new object[] { displayName, regex });

            return base.IsValid(value, validationContext);
        }

        public override bool IsValid(object value)
        {
            if (value != null && value.ToString() != "") {
                var match = Regex.Match(value.ToString(), regex, RegexOptions.IgnoreCase);

                return match.Success;
            } else
            {
                return true;
            }
        }
    }

}