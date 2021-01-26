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
using Microsoft.AspNetCore.Http;
using TicketsBasket.Services.Storage;

namespace TicketsBasket.Services
{
    public interface IUserProfilesService
    {
        Task<OperationResponse<UserProfileDetail>> GetProfileByUSerIdAsync();

        Task<OperationResponse<UserProfileDetail>> CreateProfileAsync(CreateProfileRequest model);

        Task<OperationResponse<UserProfileDetail>> UpdateProfilePicture(IFormFile image);
    }

    public class UserProfilesServices : BaseService, IUserProfilesService
    {
        private readonly IdentityOptions _identity;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageService _storageService;

        public UserProfilesServices(IdentityOptions identity, IUnitOfWork unitOfWork, IStorageService storageService)
        {
            _identity = identity;
            _unitOfWork = unitOfWork;
            _storageService = storageService;
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

        public async Task<OperationResponse<UserProfileDetail>> UpdateProfilePicture(IFormFile image)
        {
            var userProfile = await _unitOfWork.UserProfiles.GetByUserId(_identity.UserId);

            if (userProfile == null)
            {
                return Error<UserProfileDetail>("Profile not found", null);
            }

            string imageUrl = userProfile.ProfilePicture;

            try
            {
                imageUrl = await _storageService.SaveBlobAsync("users", image, BlobType.Image);


                //remove old blob
                if (userProfile.ProfilePicture != "unknown")
                {
                    await _storageService.RemoveBlobAsync("users", userProfile.ProfilePicture);
                }

                if (string.IsNullOrWhiteSpace(imageUrl))
                {
                    return Error("Image is required", userProfile.ToUserProfileDetail());
                }
            }
            catch (BadImageFormatException)
            {

                return Error("Invalid image file", userProfile.ToUserProfileDetail());
            }

            userProfile.ProfilePicture = imageUrl;

            await _unitOfWork.CommitChangesAsync();

            return Success("Profile picture updated successfully", userProfile.ToUserProfileDetail());
        }
    }
}
