using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicketsBasket.Shared.Responses;
using TicketsBasket.Shared.Models;

namespace TicketsBasket.Services
{
    public interface IUserProfileService
    {
        Task<OperationResponse<UserProfileDetail>> GetProfileByUSerIdAsync();
    }

    public class UserProfilesServices : IUserProfileService
    {
        public Task<OperationResponse<UserProfileDetail>> GetProfileByUSerIdAsync()
        {
            throw new NotImplementedException();
        }
    }
}
