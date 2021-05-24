using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class MembershipRefunds : BaseDto<int>
    {
        public int MembershipId { get; set; }
        public virtual Membership Membership { get; set; }
        public int RefundsId { get; set; }
        public virtual Refund Refunds { get; set; }
    }
}
