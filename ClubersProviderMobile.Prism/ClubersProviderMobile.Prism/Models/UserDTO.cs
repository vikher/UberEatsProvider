using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubersProviderMobile.Prism.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public int RoleId { get; set; }
        public int? SquareId { get; set; }
        public Square Square { get; set; }
        public int? CellId { get; set; }
        public Cell Cell { get; set; }
        public int ZoneId { get; set; }
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
        public string ProfileImageUrl { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public List<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();
        public Establishments Establishment { get; set; }
        public string FullName => $"{FirstName} {LastName}";

    }
}
