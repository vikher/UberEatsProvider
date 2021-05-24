using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class EstablismentSocialMedia : BaseDto<int>
    {
        public int EstablishmentId { get; set; }
        public virtual Establishments Establishment { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
    }
}
