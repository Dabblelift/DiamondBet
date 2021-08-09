namespace DiamondBet.Web.ViewModels.CustomValidation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class NotEqualToAttribute : ValidationAttribute
    {
        public NotEqualToAttribute(string otherProperty)
        {
            this.OtherProperty = otherProperty;
        }

        private string OtherProperty { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var otherPropertyValue = validationContext.ObjectType.GetProperty(this.OtherProperty)
                .GetValue(validationContext.ObjectInstance);

            if (value.ToString().Equals(otherPropertyValue.ToString()))
            {
                if (this.ErrorMessage.Length > 0)
                {
                    return new ValidationResult(this.ErrorMessage);
                }

                return new ValidationResult(
                    $"{validationContext.MemberName} cannot have the same value as {this.OtherProperty}");
            }

            return ValidationResult.Success;
        }
    }
}
