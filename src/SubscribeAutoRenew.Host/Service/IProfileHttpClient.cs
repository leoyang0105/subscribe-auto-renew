namespace SubscribeAutoRenew.Host.Service
{
    public interface IProfileHttpClient
    {
        Task<string> FetchProfile(string url);
    }
}