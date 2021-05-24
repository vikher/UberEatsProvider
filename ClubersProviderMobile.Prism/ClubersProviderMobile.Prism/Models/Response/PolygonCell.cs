using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class PolygonCell : BaseDto<int>
    {
        public string FullAddress { get; set; }
        public string LatLng { get; set; }
        public int CellId { get; set; }
        public virtual Cell Cell { get; set; }
    }
}
