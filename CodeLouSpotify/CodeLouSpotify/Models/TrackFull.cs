using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLouSpotify.Models
{
    public class TrackFull
    {
        [JsonProperty(PropertyName ="album")]
        public AlbumSimple Album { get; set; }
        [JsonProperty(PropertyName ="artists")]
        public ArtistFull[] Artists { get; set; }
        [JsonProperty(PropertyName ="available_markets")]
        public string[] AvailableMarkets { get; set; }
        [JsonProperty(PropertyName ="disc_number")]
        public int DiscNum { get; set; }
        [JsonProperty(PropertyName ="duration_ms")]
        public int Duration { get; set; }
        [JsonProperty(PropertyName ="explicit")]
        public bool Explicit { get; set; }
        [JsonProperty(PropertyName ="external_ids")]
        public ExternalId ExternalIds { get; set; }
        [JsonProperty(PropertyName ="external_urls")]
        public ExternalUrl ExternalUrls { get; set; }
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName ="is_playable")]
        public bool IsPlayable { get; set; }
        [JsonProperty(PropertyName ="linked_from")]
        public TrackLink LinkedFrom { get; set; }
        [JsonProperty(PropertyName ="name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName ="popularity")]
        public int Popularity { get; set; }
        [JsonProperty(PropertyName ="preview_url")]
        public string PreveiwUrl { get; set; }
        [JsonProperty(PropertyName ="track_number")]
        public int TrackNumber { get; set; }
        [JsonProperty(PropertyName ="type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName ="uri")]
        public string Uri { get; set; }
        [JsonProperty(PropertyName ="is_local")]
        public bool IsLocal { get; set; }
    }

    public class TrackLink
    {
        [JsonProperty(PropertyName ="external_urls")]
        public ExternalUrl ExternalUrls { get; set; }
        [JsonProperty(PropertyName ="href")]
        public string Href { get; set; }
        [JsonProperty(PropertyName ="id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName ="type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName ="uri")]
        public string Uri { get; set; }

    }
}
