using System;
using Events.IO.Domain.Core.Models;
using FluentValidation;

namespace Events.IO.Domain.Events
{
    public class Address : Entity<Address>
    {
        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string ZipCode { get; private set; }
        public string City { get; private set; }
        public string Province { get; private set; }
        public Guid? EventId { get; private set; }

        //Entity Framework navigation property
        public virtual Event Event { get; private set; }

        public Address(Guid id, string address1, string address2, string zipCode, string city, string province, Guid? eventId)
        {
            Id = id;
            Address1 = address1;
            Address2 = address2;
            ZipCode = zipCode;
            City = city;
            Province = province;
            EventId = eventId;
        }

        //EF constructor
        protected Address() { }

        public override bool IsValid()
        {
            RuleFor(c => c.Address1)
                .NotEmpty().WithMessage("The Address 1 must be populated")
                .Length(10, 150).WithMessage("The Address 1 must be between 10 and 150 characters");

            RuleFor(c => c.ZipCode)
                .NotEmpty().WithMessage("The ZipCode must be populated")
                .Length(6).WithMessage("The ZipCode must have 6 characters");

            RuleFor(c => c.City)
                .NotEmpty().WithMessage("The City must be populated")
                .Length(2, 150).WithMessage("The City must be between 2 and 150 characters");

            RuleFor(c => c.Province)
                .NotEmpty().WithMessage("The Province must be populated")
                .Length(2, 150).WithMessage("The Province must be between 2 and 150 characters");

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }
    }
}