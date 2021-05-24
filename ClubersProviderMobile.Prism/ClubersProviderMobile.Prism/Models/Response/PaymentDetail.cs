using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class PaymentDetail : BaseDto<int>
    {
        public int BankId { get; set; }
        public virtual Bank Bank { get; set; }
        public string CLABE { get; set; }
        public string Email { get; set; }
        public string Manager { get; set; }
        public string Staff { get; set; }
        public string PhoneNumber { get; set; }
    }
}
