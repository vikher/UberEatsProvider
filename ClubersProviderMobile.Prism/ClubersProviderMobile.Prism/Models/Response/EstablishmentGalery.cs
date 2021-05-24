using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class EstablishmentGalery : BaseDto<int>
    {
        public int GaleryId { get; set; }
        public virtual Galeries Galeries { get; set; }

        public int EstablishmentId { get; set; }
        public virtual Establishment Establishment { get; set; }
    }
}
