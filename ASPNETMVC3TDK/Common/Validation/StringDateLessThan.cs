using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Toyota.Common.Validation
{

    public class StringDateLessThan : ValidationAttribute
    {

        public string displayName;
        public string  _comparisonProperty;
        public string _comparisonPropertyAliasName;
        public string dateFormat;
        

        public StringDateLessThan(string comparisonProperty, string dateFormat)
        {
            _comparisonProperty = comparisonProperty;
            this.dateFormat = dateFormat;
        }

        public StringDateLessThan(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
            this.dateFormat = Constant.DATE_FORMAT;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var attributes = validationContext.ObjectType.GetProperty(validationContext.MemberName).GetCustomAttributes(typeof(DisplayNameAttribute), true);
            if (attributes != null)
                this.displayName = (attributes[0] as DisplayNameAttribute).DisplayName;
            else
                this.displayName = validationContext.DisplayName;


            ErrorMessage = String.Format(Constant.MSG_DATE_SEQ, new object[] { });

            var currentValue = (string)value;

            DateTime now = DateTime.Now;
            DateTime dStart = now;
            DateTime dEnd = now;
            bool chValidityStart = true;
            if (currentValue != null)
            {
                 chValidityStart = DateTime.TryParseExact(
                   currentValue,
                   this.dateFormat,
                   CultureInfo.InvariantCulture,
                   DateTimeStyles.None,
                   out dStart);
            }
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = (string)property.GetValue(validationContext.ObjectInstance);

            bool chValidityEnd = true;
            if (comparisonValue != null)
            {
                 chValidityEnd = DateTime.TryParseExact(
                   comparisonValue,
                   this.dateFormat,
                   CultureInfo.InvariantCulture,
                   DateTimeStyles.None,
                   out dEnd);
            }

            if (chValidityStart && chValidityEnd && currentValue != null && comparisonValue  != null && dStart > dEnd)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }


    }
}