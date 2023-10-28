using SubscribeAutoRenew.Host.Models;

namespace SubscribeAutoRenew.Host.Service
{
    public interface IProfileService
    {
        Task AddProfile(ProfileModel profile);
        Task DeleteProfile(string profileId);
        Task<IEnumerable<ProfileModel>> GetProfiles();
        Task<ProfileModel> GetById(string id);
        Task UpdateProfile(ProfileModel profile);
        Task RenewProfile(string id);
    }
}