using Newtonsoft.Json;

namespace CodeLouSpotify.Models
{
    public class ExternalUrl
    {
        [JsonProperty(PropertyName ="spotify")]
        public string Spotify { get; set; }
    }
}
