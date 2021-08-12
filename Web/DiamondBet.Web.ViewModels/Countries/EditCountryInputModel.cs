namespace DiamondBet.Web.ViewModels.Countries
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class EditCountryInputModel
    {
        [Required]
        [MaxLength(50)]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        [StringLength(50, MinimumLength = 3)]
        public string Capital { get; set; }
    }
}
