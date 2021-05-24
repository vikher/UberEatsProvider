using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class PaymentMethods : BaseDto<int>
    {
        [Required]
        [MaxLength(80)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
