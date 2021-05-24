using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class LocationMaps : BaseDto<int>
    {
        public string FullAddress { get; set; }
        public string LatLng { get; set; }
        public virtual ICollection<Square> Squares { get; set; }
        public virtual ICollection<Zone> Zones { get; set; }
    }
}
