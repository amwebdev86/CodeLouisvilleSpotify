using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLouSpotify.Models
{
    public class AlbumSimple
    {
        [JsonProperty(PropertyName ="album_group")]
        public string AlbumGroup { get; set; }
        public string AlbumType { get; set; }
        [JsonProperty(PropertyName ="artists")]
        public ArtistFull[] Artists { get; set; }
        [JsonProperty(PropertyName ="available_markets")]
        public string[] AvailableMarkets { get; set; }
        [JsonProperty(PropertyName ="external_urls")]
        public ExternalUrl ExternalUrls { get; set; }
        [JsonProperty(PropertyName ="href")]
        public string Href { get; set; }
        [JsonProperty(PropertyName ="id")]
        public string Id { get; set; }
        public SpotifyImage[] Images { get; set; }
        [JsonProperty(PropertyName ="name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName ="release_date")]
        public string ReleaseDate { get; set; }
        [JsonProperty(PropertyName ="release_date_precision")]
        public string ReleaseDatePrecision { get; set; }
        [JsonProperty(PropertyName ="type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName ="uri")]
        public string Uri { get; set; }
    }
}
