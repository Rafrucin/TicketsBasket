using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicketsBasket.Shared.Responses;
using TicketsBasket.Shared.Models;
using TicketsBasket.Infrastructure.Options;
using TicketsBasket.Repositories;
using TicketsBasket.Models.Mappers;
using TicketsBasket.Shared.Requests;
using Newtonsoft.Json;
using System.Linq;
using TicketsBasket.Models.Domain;
using System.Security.Claims;

namespace TicketsBasket.Services
{
    public interface IUserProfilesService
    {
        Task<OperationResponse<UserProfileDetail>> GetProfileByUSerIdAsync();

        Task<OperationResponse<UserProfileDetail>> CreateProfileAsync(CreateProfileRequest model);
    }

    public class UserProfilesServices : BaseService, IUserProfilesService
    {
        private readonly IdentityOptions _identity;
        private readonly IUnitOfWork _unitOfWork;

        public UserProfilesServices(IdentityOptions identity, IUnitOfWork unitOfWork)
        {
            _identity = identity;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResponse<UserProfileDetail>> CreateProfileAsync(CreateProfileRequest model)
        {
            var user = _identity.User;

            var city = user.FindFirst("city").Value;
            var country = user.FindFirst("country").Value;
            var firstName = user.FindFirst(ClaimTypes.GivenName).Value;
            var lastName = user.FindFirst(ClaimTypes.Surname).Value;
            var fullName = user.FindFirst("name").Value;
            var email = user.FindFirst("emails").Value;

            // TODO: upload pic to blob storage
            string profilePictureUrl = "unknown";

            var newUser = new UserProfile
            {
                Country = country,
                City = city,
                CreatedOn = DateTime.UtcNow,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Id = Guid.NewGuid().ToString(),
                UserId = _identity.UserId,
                IsOrganizer = model.IsOrganizer,
                ProfilePicture = profilePictureUrl
            };

            await _unitOfWork.UserProfiles.CreateAsync(newUser);
            await _unitOfWork.CommitChangesAsync();

            return Success("User profile created successfully!", newUser.ToUserProfileDetail());

        }

        public async Task<OperationResponse<UserProfileDetail>> GetProfileByUSerIdAsync()
        {
            var userProfile = await _unitOfWork.UserProfiles.GetByUserId(_identity.UserId);

            if (userProfile == null)
            {
                return Error<UserProfileDetail>("Profile not found", null);
            }

            return Success("Profile retrived successfully", userProfile.ToUserProfileDetail());
           
        }
    }
}
