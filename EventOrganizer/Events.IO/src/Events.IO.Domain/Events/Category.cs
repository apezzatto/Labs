using System;
using System.Collections.Generic;
using Events.IO.Domain.Core.Models;

namespace Events.IO.Domain.Events
{
    public class Category : Entity<Category>
    {

        public string Name { get; private set; }

        //Navigation property (EF)
        public virtual ICollection<Event> Events { get; set; }

        public Category(Guid id)
        {
            Id = id;
        }

        //EF Constructor
        protected Category() { }

        public override bool IsValid()
        {
            return true;
        }
    }
}