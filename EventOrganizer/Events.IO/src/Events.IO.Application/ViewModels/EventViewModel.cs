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

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        [MinLength(2, ErrorMessage = "Minimum character required: {1}")]
        [MaxLength(150, ErrorMessage = "Maximum character required: {1}")]
        public string Name { get; set; }

        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Free")]
        public bool IsFree { get; set; }

        [Display(Name = "Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }

        [Display(Name = "Online")]
        public bool Online { get; set; }

        [Display(Name = "Organizer")]
        public string CompanyName { get; set; }

        public AddressViewModel Address { get; set; }
        public CategoryViewModel Category { get; set; }
        public Guid CategoryId { get; set; }
        public Guid OrganizerId { get; set; }

        public EventViewModel()
        {
            Id = Guid.NewGuid();
            Address = new AddressViewModel();
            Category = new CategoryViewModel();
        }
    }
}
