﻿using System.ComponentModel.DataAnnotations;

namespace Events.IO.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [StringLength(9)]
        [Required(ErrorMessage = "SIN is required")]
        public string SIN { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email format")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
