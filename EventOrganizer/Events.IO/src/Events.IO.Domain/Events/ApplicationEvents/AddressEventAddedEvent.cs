﻿using System;
using System.Collections.Generic;
using System.Text;
using Events.IO.Domain.Core.AppEvents;

namespace Events.IO.Domain.Events.ApplicationEvents
{
    public class AddressEventAddedEvent : ApplicationCoreEvent
    {
        public Guid Id { get; private set; }
        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string ZipCode { get; private set; }
        public string City { get; private set; }
        public string Province { get; private set; }

        public AddressEventAddedEvent(Guid addressId, string address1, string address2, string zipCode, string city, string province, Guid eventId)
        {
            Id = addressId;
            Address1 = address1;
            Address2 = address2;
            ZipCode = zipCode;
            City = city;
            Province = province;
            AggregateId = eventId;
        }

    }
}
