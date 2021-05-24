using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Cell : BaseDto<int>
    {
        public string CellName { get; set; }
        public int? SquareId { get; set; }
        public virtual Square Square { get; set; }
        public int? ZoneId { get; set; }
        public virtual Zone Zone { get; set; }
        public virtual ICollection<PolygonCell> PolygonCell { get; set; }
        public string LatLng { get; set; }
        public string ZonaAssociate { get; set; }
        public string SquareAssociate { get; set; }
    }
}
