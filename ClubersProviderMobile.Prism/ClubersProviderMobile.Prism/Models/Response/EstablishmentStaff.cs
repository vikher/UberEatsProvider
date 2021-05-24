using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class EstablishmentStaff : BaseDto<int>
    {
        public int PersonalType { get; set; }
        public int EstablishmentId { get; set; }
        public virtual Establishments Establishment { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string INEFrontUrl { get; set; }
        public string INEBackUrl { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string IconUrl { get; set; }
    }
}
