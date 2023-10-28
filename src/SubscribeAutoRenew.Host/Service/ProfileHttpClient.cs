namespace SubscribeAutoRenew.Host.Service
{
    public class ProfileHttpClient : IProfileHttpClient
    {
        private readonly HttpClient _httpClient;
        public ProfileHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> FetchProfile(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
