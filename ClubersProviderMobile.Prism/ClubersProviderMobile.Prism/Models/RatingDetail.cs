using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class RatingDetail : BaseDto<int>
    {
        public int? RatingId { get; set; }
        public virtual Rating Rating { get; set; }

        public int OrderId { get; set; }
        public virtual Orders Order { get; set; }

        public int Score { get; set; }

        public string Comments { get; set; }
    }
}
