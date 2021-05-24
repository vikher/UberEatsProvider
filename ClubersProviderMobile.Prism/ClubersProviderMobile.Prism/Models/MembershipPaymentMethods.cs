using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class MembershipPaymentMethods : BaseDto<int>
    {
        public int MembershipId { get; set; }
        public virtual Membership Membership { get; set; }
        public int PaymentMethodId { get; set; }
        public virtual PaymentMethods PaymentMethods { get; set; }
    }
}
