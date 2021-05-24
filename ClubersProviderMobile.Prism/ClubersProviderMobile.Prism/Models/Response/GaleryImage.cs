using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class GaleryImage : BaseDto<int>
    {
        public int GaleryId { get; set; }
        public virtual Galeries Galeries { get; set; }

        public string ImgUrl { get; set; }
    }
}
