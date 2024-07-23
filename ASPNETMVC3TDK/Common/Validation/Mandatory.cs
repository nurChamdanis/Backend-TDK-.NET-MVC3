using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Toyota.Common.Validation
{
    public class Mandatory : ValidationAttribute
    {
        
        public string displayName;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            var attributes = validationContext.ObjectType.GetProperty(validationContext.MemberName).GetCustomAttributes(typeof(DisplayNameAttribute), true);
            if (attributes != null)
                this.displayName = (attributes[0] as DisplayNameAttribute).DisplayName;
            else
                this.displayName = validationContext.DisplayName;

            ErrorMessage = String.Format(Constant.MSG_MANDATORY, new object[] { displayName });
            return base.IsValid(value, validationContext);
        }

        public override bool IsValid(object value)
        {
            return value != null && value.ToString() != "";
        }
    }
}