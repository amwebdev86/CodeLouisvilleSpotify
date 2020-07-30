using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLouSpotify.Models
{

    public class Profile
    {
        [JsonProperty(PropertyName = "display_name")]
        public string DisplayName { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "external_urls")]
        public External_Urls ExternalUrls { get; set; }
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "images")]
        public Image[] Images { get; set; }
        [JsonProperty(PropertyName = "product")]
        public string Product { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }
    }

    public class External_Urls
    {
        [JsonProperty(PropertyName = "spotify")]
        public string Spotify { get; set; }
    }

    public class Image
    {
        [JsonProperty(PropertyName = "height")]
        public object Height { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        [JsonProperty(PropertyName = "width")]
        public object Width { get; set; }
    }

}
