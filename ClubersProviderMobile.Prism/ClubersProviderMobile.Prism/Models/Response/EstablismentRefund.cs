using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class EstablismentRefund : BaseDto<int>
    {
        public int EstablishmentId { get; set; }
        public virtual Establishments Establishment { get; set; }
        public int RefundId { get; set; }
        public virtual Refund Refund { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
    }
}
