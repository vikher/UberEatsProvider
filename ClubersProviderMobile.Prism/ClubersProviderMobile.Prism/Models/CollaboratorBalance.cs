using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class CollaboratorBalance
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public EstablishmentStaff Collaborator { get; set; }
    }
}
