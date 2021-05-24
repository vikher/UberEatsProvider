using System;
using System.Collections.Generic;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class Collaborator
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string FullName => $"{FirstName} {MiddleName} {LastName} {SecondLastName}";
        public string ImageUrl { get; set; }
        public List<Order> Orders { get; set; }
    }
}
