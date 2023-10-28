using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SubscribeAutoRenew.Host.Models
{
    public class ClashProfileModel
    {
        [JsonProperty("mixed-port")]
        public int MixedPort { get; set; }
        [JsonProperty("allow-lan")]
        public bool AllowLan { get; set; }
        [JsonProperty("bind-address")]
        public string BindAddress { get; set; }
        [JsonProperty("mode")]
        public string Mode { get; set; }
        [JsonProperty("log-level")]
        public string LogLevel { get; set; }
        [JsonProperty("external-controller")]
        public string ExternalController { get; set; }
        [JsonProperty("proxies")]
        public JArray Proxies { get; set; }
        [JsonProperty("proxy-groups")]
        public JArray ProxyGroups { get; set; }
        [JsonProperty("rules")]
        public JArray Rules { get; set; }
    }
}
