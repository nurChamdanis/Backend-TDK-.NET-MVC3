using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Toyota.Common.Validation
{
    public class NumericPrecisionAndScale : RegexAtt
    {

        protected int precision;
        protected int scale;
        public NumericPrecisionAndScale(int precision, int scale) : base($@"^(0|-?\d{{0,{precision - scale}}}(\.\d{{0,{scale}}})?)$")
        {
            this.precision = precision;
            this.scale = scale;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var attributes = validationContext.ObjectType.GetProperty(validationContext.MemberName).GetCustomAttributes(typeof(DisplayNameAttribute), true);
            if (attributes != null)
                this.displayName = (attributes[0] as DisplayNameAttribute).DisplayName;
            else
                this.displayName = validationContext.DisplayName;

            //ErrorMessage = MessageRepository.Instance.getTextMessage("MSLSSTD062E", new object[] { displayName, precision+","+scale });
            ErrorMessage = String.Format(Constant.MSG_FORMAT, new object[] { displayName, precision + "," + scale });

            return base.IsValid(value, validationContext);
        }
    }
}