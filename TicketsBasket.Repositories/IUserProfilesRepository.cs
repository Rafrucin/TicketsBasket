using System;
using System.Text;
using System.Threading.Tasks;
using TicketsBasket.Models.Domain;

namespace TicketsBasket.Repositories
{
    public interface IUserProfilesRepository
    {
        Task CreateAsync(UserProfile userProfile);

        Task<UserProfile> GetByIdAsync(string id);

        Task<UserProfile> GetByUserId(string id);

        void Remove(UserProfile userProfile);

        void Update(UserProfile userProfile);
    }
}
