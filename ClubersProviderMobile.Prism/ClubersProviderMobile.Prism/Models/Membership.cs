using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Membership : BaseDto<int>
    {
        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
        public int MembershipValidity { get; set; }
        public string MembershipValidityText { get; set; }
        public decimal Price { get; set; }
        public float Discount { get; set; }
        public int TrialPeriod { get; set; }
        public int ClubersCashVilidity { get; set; }
        public int BalanceValidity { get; set; }
        public List<int> PaymentMethodsIds { get; set; } = new List<int>();
        public List<int> RefundsIds { get; set; } = new List<int>();
        public virtual ICollection<MembershipPaymentMethods> MembershipPaymentMethods { get; set; }
        public virtual ICollection<MembershipRefunds> MembershipRefunds { get; set; }
    }
}
