using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicketsBasket.Shared.Responses;
using TicketsBasket.Shared.Models;
using TicketsBasket.Infrastructure.Options;
using TicketsBasket.Repositories;
using TicketsBasket.Models.Mappers;

namespace TicketsBasket.Services
{
    public interface IUserProfilesService
    {
        Task<OperationResponse<UserProfileDetail>> GetProfileByUSerIdAsync();
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
