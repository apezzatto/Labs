using System;
using System.ComponentModel.DataAnnotations;

namespace Events.IO.Application.ViewModels
{
    public class OrganizerViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(9)]
        [Required(ErrorMessage = "SIN is required")]
        public string SIN { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email format")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        //public OrganizerViewModel(Guid id, string sin, string name, string email)
        //{
        //    Id = id;
        //    SIN = sin;
        //    Name = name;
        //    Email = email;
        //}
    }
}