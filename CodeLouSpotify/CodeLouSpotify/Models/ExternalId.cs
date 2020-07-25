using Newtonsoft.Json;

namespace CodeLouSpotify.Models
{
    public class ExternalId
    {
        [JsonProperty(PropertyName ="isrc")]
        public string ISRC { get; set; }

    }
}
