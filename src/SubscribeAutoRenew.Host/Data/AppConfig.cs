using SubscribeAutoRenew.Host.Models;

namespace SubscribeAutoRenew.Host.Data
{
    public class AppConfig
    {
        public List<ProfileModel> Profiles { get; set; } = new List<ProfileModel>();
    }
}
