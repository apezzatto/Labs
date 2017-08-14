using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Text;

namespace Events.IO.Application.ViewModels
{
    public class EventViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Event name")]
        [Required(ErrorMessage = "Name is required")]
        [MinLength(2, ErrorMessage = "Minimum character required: {1}")]
        [MaxLength(150, ErrorMessage = "Maximum character required: {1}")]
        public string Name { get; set; }

        [Display(Name = "Event short description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Event long description")]
        public string LongDescription { get; set; }

        [Display(Name = "Event start date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Event end date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Free")]
        public bool IsFree { get; set; }

        [Display(Name = "Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }

        public bool Online { get; set; }

        [Display(Name = "Company / Organizer group name")]
        public string CompanyName { get; set; }

        public AddressViewModel Address { get; set; }
        public CategoryViewModel Category { get; set; }
        public Guid IdCategory { get; set; }
        public Guid IdOrganizer { get; set; }

        public EventViewModel()
        {
            Id = Guid.NewGuid();
            Address = new AddressViewModel();
            Category = new CategoryViewModel();
        }
    }
}
