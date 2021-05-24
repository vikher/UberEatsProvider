using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class EstablishmentFiles : BaseDto<int>
    {
        public int FileId { get; set; }
        public virtual Files Files { get; set; }
        public int EstablishmentId { get; set; }
        public virtual Establishments Establishment { get; set; }
    }
}
