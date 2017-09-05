using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Events.IO.Domain.Interfaces
{
    public interface IUser
    {
        string Name { get; }
        Guid GetUserId();
        bool IsAuthenticated();
        IEnumerable<Claim> GetClamsIdentity();
    }
}