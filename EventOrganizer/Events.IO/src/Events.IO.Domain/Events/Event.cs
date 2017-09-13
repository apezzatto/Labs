using Events.IO.Domain.Organizers;
using Events.IO.Domain.Core.Models;
using System.Collections.Generic;
using System;
using FluentValidation;

namespace Events.IO.Domain.Events
{
    public class Event : Entity<Event>
    {
        public string Name { get; private set; }
        public string ShortDescription { get; private set; }
        public string LongDescription { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public bool IsFree { get; private set; }
        public decimal Price { get; private set; }
        public bool Online { get; private set; }
        public string CompanyName { get; private set; }
        public bool Excluded { get; private set; }
        public ICollection<Tags> Tags { get; private set; }
        public Guid? CategoryId { get; private set; }
        public Guid? OrganizerId { get; private set; }
        public Guid AddressId { get; private set; }

        //Entity Framework navigation properties
        public virtual Category Category { get; private set; }
        public virtual Address Address { get; private set; }
        public virtual Organizer Organizer { get; private set; }

        public void SetAddress(Address address)
        {
            if (!address.IsValid()) return;

            Address = address;
        }

        public void SetCategory(Category category)
        {
            if (!category.IsValid()) return;

            Category = category;
        }

        public void SetEventExcluded()
        {
            //TODO: Should validate some additional business rules
            Excluded = true;
        }

        internal Event(
            string name,
            string shortDescription,
            string longDescription,
            DateTime startDate,
            DateTime endDate,
            bool isFree,
            decimal price,
            bool online,
            string companyName)
        {
            Id = Guid.NewGuid();
            Name = name;
            ShortDescription = shortDescription;
            LongDescription = longDescription;
            StartDate = startDate;
            EndDate = endDate;
            IsFree = isFree;
            Price = price;
            Online = online;
            CompanyName = companyName;
        }

        private Event() { }

        public override bool IsValid()
        {
            Validate();
            return ValidationResult.IsValid;
        }

        #region Validations
        private void Validate()
        {
            NameValidator();
            PriceValidator();
            EventDateValidator();
            LocationValidator();
            CompanyNameValidator();
            base.ValidationResult = Validate(this);

            //Aditional validations
            AddressValidator();
        }

        private void NameValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("The event name must be provided")
                .Length(2, 150).WithMessage("The event name must have between 2 and 150 characters");
        }

        private void PriceValidator()
        {
            if (!IsFree)
                RuleFor(c => c.Price)
                    .ExclusiveBetween(1, 50000)
                    .WithMessage("The price must be between $1 and $50,000");

            if (IsFree)
                RuleFor(c => c.Price)
                    .Equal(0).When(c => c.IsFree)
                    .WithMessage("The price must be 0 if the event is free");
        }

        private void EventDateValidator()
        {
            RuleFor(c => c.EndDate.Date)
                .GreaterThanOrEqualTo(c => c.StartDate.Date)
                .WithMessage("The event End Date must be bigger than or equal to the event Start Date");

            RuleFor(c => c.StartDate)
                .GreaterThan(DateTime.Now)
                .WithMessage("The event Start Date cannot be less than the Current Date");
        }

        private void LocationValidator()
        {
            if (Online)
                RuleFor(c => c.Address)
                    .Null().When(c => c.Online)
                    .WithMessage("The event cannot have a physical address if it is Online");

            if (!Online)
                RuleFor(c => c.Address)
                    .NotNull().When(c => !c.Online)
                    .WithMessage("The event must have an Address");
        }

        private void CompanyNameValidator()
        {
            RuleFor(c => c.CompanyName)
                .NotEmpty().WithMessage("The host name must be informed")
                .Length(2, 150).WithMessage("The host name must have between 2 and 150 characters");
        }

        private void AddressValidator()
        {
            if (Online) return;
            if (Address.IsValid()) return;

            foreach (var error in Address.ValidationResult.Errors)
            {
                ValidationResult.Errors.Add(error);
            }
        }

        #endregion

        public static class EventFactory
        {

            public static Event NewFullEvent(Guid id, string name, string shortDescription, string longDescription, DateTime startDate,DateTime endDate,bool isFree,decimal price,bool online,string companyName, Guid? organizerId, Address address, Guid categoryId)
            {
                var @event = new Event()
                {
                    Id = id,
                    Name = name,
                    ShortDescription = shortDescription,
                    LongDescription = longDescription,
                    StartDate = startDate,
                    EndDate = endDate,
                    IsFree = isFree,
                    Price = price,
                    Online = online,
                    CompanyName = companyName,
                    Address = address,
                    CategoryId = categoryId
                };

                if (organizerId.HasValue)
                    @event.OrganizerId = organizerId.Value;

                if (online)
                    @event.Address = null;

                return @event;
            }
        }
    }
}
