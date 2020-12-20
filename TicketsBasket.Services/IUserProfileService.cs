using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicketsBasket.Shared.Responses;
using TicketsBasket.Shared.Models;
using TicketsBasket.Infrastructure.Options;

namespace TicketsBasket.Services
{
    public interface IUserProfileService
    {
        Task<OperationResponse<UserProfileDetail>> GetProfileByUSerIdAsync();
    }

    public class UserProfilesServices : IUserProfileService
    {
        private readonly IdentityOptions _options;

        public UserProfilesServices(IdentityOptions options)
        {
            _options = options;
        }
        public Task<OperationResponse<UserProfileDetail>> GetProfileByUSerIdAsync()
        {
            throw new NotImplementedException();
        }
    }
}
