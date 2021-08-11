namespace DiamondBet.Web.ViewModels.CustomValidation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CurrentYearMaxValueAttribute : ValidationAttribute
    {
        private readonly int minYear;

        public CurrentYearMaxValueAttribute(int minYear)
        {
            this.minYear = minYear;
            this.ErrorMessage = $"Year should be a number between {minYear} and {DateTime.UtcNow.Year}";
        }

        public override bool IsValid(object value)
        {
            if (value is int intValue)
            {
                if (intValue <= DateTime.UtcNow.Year && intValue >= minYear)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
