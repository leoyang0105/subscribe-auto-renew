using SubscribeAutoRenew.Host.Data;

namespace SubscribeAutoRenew.Host.Service
{
    public interface IConfigService
    {
        Task CreateOrUpdate(AppConfig config);
        void Delete();
        Task<AppConfig> Get();
    }
}