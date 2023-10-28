using Newtonsoft.Json;

namespace SubscribeAutoRenew.Host.Models
{
    public class ProfileModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Path { get; set; } = "/clash/config.yaml";
        public int MixedPort { get; set; } = 7890;
        public bool AllowLan { get; set; } = true;
        public string BindAddress { get; set; } = "*";
        public string Mode { get; set; } = "rule";
        public string ExternalController { get; set; } = "0.0.0.0:9090";
        public string LogLevel { get; set; } = "info";
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
