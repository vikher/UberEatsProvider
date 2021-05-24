using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Establishment
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public List<Collaborator> Collaborators { get; set; }
        public Address Address { get; set; }
        public DateTime HomeServiceOpenHours { get; set; }
        public DateTime HomeServiceCloseHours { get; set; }
        public DateTime OnSiteServiceOpenHours { get; set; }
        public DateTime OnSiteServiceCloseHours { get; set; }

    }
}
