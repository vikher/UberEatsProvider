using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Customers : BaseDto<int>
    {
        public string INE { get; set; }

        public int MembershipId { get; set; }
        public virtual Membership Membership { get; set; }

        public int UserId { get; set; }
        public virtual UserDTO User { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual IEnumerable<Orders> Orders { get; set; }
    }
}
