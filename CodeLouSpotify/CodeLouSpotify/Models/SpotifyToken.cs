using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLouSpotify.Models
{

    public class SpotifyToken
    {
        [JsonProperty(PropertyName="access_token")]
        public string AccessToken { get; set; }
        [JsonProperty(PropertyName="token_type")]
        public string TokenType { get; set; }
        [JsonProperty(PropertyName="scope")]
        public string Scope { get; set; }
        [JsonProperty(PropertyName="expires_in")]
        public int Expiration { get; set; }
        [JsonProperty(PropertyName="refresh_token")]
        public string RefreshToken { get; set; }
    }

}
