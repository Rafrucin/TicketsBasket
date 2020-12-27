using System;
using System.Collections.Generic;
using System.Text;
using TicketsBasket.Models.Domain;
using TicketsBasket.Shared.Models;

namespace TicketsBasket.Models.Mappers
{
    public static class UserProfileMapper
    {
        public static UserProfileDetail ToUserProfileDetail(this UserProfile userProfile)
        {
            return new UserProfileDetail
            {
                Id = userProfile.Id,
                Country = userProfile.Country,
                Email = userProfile.Email,
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                IsOrganizer = userProfile.IsOrganizer,
                UserId = userProfile.UserId,
                ProfilePicture = userProfile.ProfilePicture,
                CreatedSince = "1m"
            };
        }
    }
}
