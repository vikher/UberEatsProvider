using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class EstablishmentAmenities : BaseDto<int>
    {
        public int AmenityId { get; set; }
        public virtual Amenity Amenity { get; set; }
        public int EstablishmentId { get; set; }
        public virtual Establishments Establishment { get; set; }
    }
}
