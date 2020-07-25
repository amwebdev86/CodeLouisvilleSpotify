using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLouSpotify.Models
{
    public class SpotifyPaging 
    {
        [JsonProperty(PropertyName ="href")]
        public string Href { get; set; }

        [JsonProperty(PropertyName ="items")]
        public Item1[] Items { get; set; }

        [JsonProperty(PropertyName ="limit")]
        public int Limit { get; set; }

        [JsonProperty(PropertyName ="next")]
        public string Next { get; set; }

        [JsonProperty(PropertyName ="offset")]
        public int Offset { get; set; }

        [JsonProperty(PropertyName ="previous")]
        public string Previous { get; set; }

        [JsonProperty(PropertyName ="total")]
        public int Total { get; set; }

    }

    public class Item1
    {
       public List<TrackFull> Tracks { get; set; }
    }

 


}
