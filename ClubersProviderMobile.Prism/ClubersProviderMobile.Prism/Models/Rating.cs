using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Rating : BaseDto<int>
    {
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<RatingDetail> RatingDetails { get; set; }
    }
}
