using System;
using System.Security.Claims;

namespace Events.IO.Infra.CrossCutting.Identity.Models
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Get ID from connected user
        /// </summary>
        /// <param name="principal">Represents the connected AspNetUser</param>
        /// <returns></returns>
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);

            return claim?.Value;
        }
    }
}