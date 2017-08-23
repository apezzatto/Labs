using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Events.IO.Application.ViewModels
{
    public class AddressViewModel
    {
        //Address is not mandatory. No Data Annotations.

        [Key]
        public Guid Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public Guid EventId { get; set; }

        public AddressViewModel()
        {
            Id = Guid.NewGuid();
        }

        public SelectList Provinces()
        {
            return new SelectList(ProvinceViewModel.GetProvincesList(), "PA", "Name");
        }

        public override string ToString()
        {
            return Address1 + ", " + (Address2 != String.Empty ? Address2 + ", " : String.Empty) + City + ", " + Province + " - " + ZipCode;
        }
    }
}
