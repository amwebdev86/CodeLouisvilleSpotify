using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLouSpotify.Models
{
    public class SpotifyUser
    {
        public string ClientId { get; private set; } = "9f6e8509f8b34c5bb57f93f3561e705a";
        public string ClientSecret { get; private set; } = "";
        public string CallbackUri { get; set; } = "https://localhost:44363/callback";
    }
}
