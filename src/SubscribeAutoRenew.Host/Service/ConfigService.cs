using SubscribeAutoRenew.Host.Data;
using System.Text;
using System.Text.Json;

namespace SubscribeAutoRenew.Host.Service
{
    public class ConfigService : IConfigService
    {
        private readonly ILogger _logger;
        private readonly string _path;
        private AppConfig _appConfig;
        public ConfigService(
            IConfiguration configuration,
            IHostEnvironment env,
            ILogger<ConfigService> logger)
        {
            _path = string.IsNullOrEmpty(configuration["AppConfig"]) ? Path.Combine(env.ContentRootPath, "config.json") : configuration["AppConfig"];
            _logger = logger;

        }
        public async Task<AppConfig> Get()
        {
            if (_appConfig == null && File.Exists(_path))
            {
                var content = await File.ReadAllTextAsync(_path);
                _appConfig = JsonSerializer.Deserialize<AppConfig>(content);
            }
            return _appConfig ?? new AppConfig();

        }
        public async Task CreateOrUpdate(AppConfig config)
        {
            if (!Directory.Exists(Path.GetDirectoryName(_path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_path));
            }
            using var fs = new FileStream(_path, FileMode.Create);
            byte[] textBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(config));
            await fs.WriteAsync(textBytes, 0, textBytes.Length);
            _appConfig = config;
        }

        public void Delete()
        {
            _appConfig = null;
            if (!File.Exists(_path))
                return;
            File.Delete(_path);
        }

    }
}
