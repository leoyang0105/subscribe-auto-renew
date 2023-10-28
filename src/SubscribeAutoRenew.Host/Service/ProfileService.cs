using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SubscribeAutoRenew.Host.Models;
using System.Dynamic;
using System.Text;
using YamlDotNet.Serialization;

namespace SubscribeAutoRenew.Host.Service
{
    public class ProfileService : IProfileService
    {
        private readonly ILogger _logger;
        private readonly IConfigService _configService;
        private readonly IProfileHttpClient _profileHttpClient;
        public ProfileService(
            ILogger<ProfileService> logger,
            IConfigService configService,
            IProfileHttpClient profileHttpClient)
        {
            _logger = logger;
            _configService = configService;
            _profileHttpClient = profileHttpClient;
        }
        public async Task<IEnumerable<ProfileModel>> GetProfiles()
        {
            var config = await _configService.Get();
            return config.Profiles.OrderBy(x => x.DisplayOrder);
        }
        public async Task AddProfile(ProfileModel profile)
        {
            if (profile.Id == null)
            {
                profile.Id = System.Guid.NewGuid().ToString();
            }
            profile.CreatedAt = DateTime.UtcNow;
            profile.ModifiedAt = DateTime.UtcNow;
            var config = await _configService.Get();
            config.Profiles.Add(profile);
            await _configService.CreateOrUpdate(config);
            await RenewProfile(profile.Id);
        }
        public async Task UpdateProfile(ProfileModel profile)
        {
            profile.ModifiedAt = DateTime.UtcNow;
            var config = await _configService.Get();
            config.Profiles.Remove(config.Profiles.FirstOrDefault(x => x.Id == profile.Id));
            config.Profiles.Add(profile);
            await _configService.CreateOrUpdate(config);
        }
        public async Task DeleteProfile(string profileId)
        {
            var config = await _configService.Get();
            config.Profiles.Remove(config.Profiles.FirstOrDefault(x => x.Id == profileId));
            await _configService.CreateOrUpdate(config);
        }

        public async Task<ProfileModel> GetById(string id)
        {
            var profiles = await GetProfiles();
            return profiles.FirstOrDefault(x => x.Id == id);
        }
        public async Task RenewProfile(string id)
        {
            var profile = await GetById(id);
            try
            {
                var yaml = await _profileHttpClient.FetchProfile(profile.Link);
                var deserializer = new Deserializer();
                var yamlObj = deserializer.Deserialize(yaml);
                var js = new JsonSerializer();

                var w = new StringWriter();
                js.Serialize(w, yamlObj);
                var model = JsonConvert.DeserializeObject<ClashProfileModel>(w.ToString());
                model.MixedPort = profile.MixedPort;
                model.ExternalController = profile.ExternalController;
                model.AllowLan = profile.AllowLan;
                model.LogLevel = profile.LogLevel;
                model.BindAddress = profile.BindAddress;
                model.Mode = profile.Mode;

                string jsonText = JsonConvert.SerializeObject(model);
                var expConverter = new ExpandoObjectConverter();
                dynamic deserializedObject = JsonConvert.DeserializeObject<ExpandoObject>(jsonText, expConverter);

                var serializer = new Serializer();
                string result = serializer.Serialize(deserializedObject);
                if (!Directory.Exists(Path.GetDirectoryName(profile.Path)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(profile.Path));
                }
                using var fs = new FileStream(profile.Path, FileMode.Create);
                byte[] textBytes = Encoding.UTF8.GetBytes(result);
                fs.Write(textBytes, 0, textBytes.Length);
                await UpdateProfile(profile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Failed to refresh profile.");
            }
        }
    }
}
