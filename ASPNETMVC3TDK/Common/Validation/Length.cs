using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Toyota.Common.Validation;

namespace Toyoya.Common.Validation
{
    public class Length : ValidationAttribute

    {
        string displayName;
        private int maxLength;

        private int minLength;

        public Length(int max)
        {
            this.maxLength = max;
        }
        public Length (int max, int min)
        {
            this.maxLength = max;
            this.minLength = min;
        }

      

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var attributes = validationContext.ObjectType.GetProperty(validationContext.MemberName).GetCustomAttributes(typeof(DisplayNameAttribute), true);
            if (attributes != null)
                this.displayName = (attributes[0] as DisplayNameAttribute).DisplayName;
            else
                this.displayName = validationContext.DisplayName;
            // ErrorMessage = MessageRepository.Instance.getTextMessage("MSLSSTD060E", new object[] { displayName, maxLength.ToString() });
            ErrorMessage = String.Format(Constant.MSG_LENGTH, new object[] { displayName, maxLength.ToString() });

            return base.IsValid(value, validationContext);
        }

        public override bool IsValid(object value)
        {
            return value == null || (value != null && value.ToString().Length <= maxLength);
        }
    }
}