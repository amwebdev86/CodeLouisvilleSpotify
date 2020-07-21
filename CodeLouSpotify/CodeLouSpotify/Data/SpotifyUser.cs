using DotNetEnv;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLouSpotify.Models
{
    public class SpotifyUser
    {
        public string ClientId { get; private set; } 
        public string ClientSecret { get; private set; } 
        public string CallbackUri { get; set; } = "https://localhost:44363/callback";
        public SpotifyUser(string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }
        public SpotifyUser()
        {
            Env.Load(@"C:\Users\AMweb\source\repos\amwebdev86\CodeLouisvilleSpotify\CodeLouSpotify\CodeLouSpotify\.env");
            ClientId = "9f6e8509f8b34c5bb57f93f3561e705a";
            ClientSecret = Env.GetString("CLIENT_SECRET");
        }

        public string Authorize()
        {
            var qb = new QueryBuilder();
            qb.Add("response_type", "code");
            qb.Add("client_id", ClientId);
            qb.Add("scope", "user-read-private user-read-email");
            qb.Add("redirect_uri", CallbackUri);
            return $@"https://accounts.spotify.com/authorize{qb.ToQueryString()}";
        }
    }
}
