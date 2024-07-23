using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Toyota.Common.Validation
{
    public class StringDateFormat: ValidationAttribute
    {
        public string displayName;

        public string dateFormat;

        public StringDateFormat ()
        {
            
            this.dateFormat = Constant.DATE_FORMAT;
        }

        public StringDateFormat(string dateFormat)
        {
            this.dateFormat = dateFormat;
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            var attributes = validationContext.ObjectType.GetProperty(validationContext.MemberName).GetCustomAttributes(typeof(DisplayNameAttribute), true);
            if (attributes != null)
                this.displayName = (attributes[0] as DisplayNameAttribute).DisplayName;
            else
                this.displayName = validationContext.DisplayName;

            ErrorMessage = String.Format(Constant.MSG_FORMAT, new object[] { displayName, dateFormat });

            return base.IsValid(value, validationContext);
        }

        public override bool IsValid(object value)
        {
            DateTime d;
            bool chValidity = true;
            if (value != null)
            {
                chValidity = DateTime.TryParseExact(
                   value.ToString(),
                   dateFormat,
                   CultureInfo.InvariantCulture,
                   DateTimeStyles.None,
                   out d);
            }
            return chValidity;
        }
        
    }
}