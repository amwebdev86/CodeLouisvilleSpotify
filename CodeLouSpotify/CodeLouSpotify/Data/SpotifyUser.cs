using DotNetEnv;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLouSpotify.Models
{
    /// <summary>
    /// Class to store the Client ID and Secret from Spotify.
    /// </summary>
    public class SpotifyUser
    {
        public string ClientId { get; private set; } 
        public string ClientSecret { get; private set; } 
        public string CallbackUri { get; set; } = "https://localhost:44363/callback";
        /// <summary>
        /// Creates new instance of ClientId and Secret
        /// </summary>
        /// <param name="clientId">Enter your Client Id from https://developer.spotify.com/ Spotify</param>
        /// <param name="clientSecret">Enter your Client Secret from https://developer.spotify.com/ Spotify</param>
        public SpotifyUser(string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }
        public SpotifyUser()
        {
            //TODO: Remove reference to env files and hard code values for Code Lou to use.
            //Env.Load(@"C:\Users\AMweb\source\repos\amwebdev86\CodeLouisvilleSpotify\CodeLouSpotify\CodeLouSpotify\.env");
            //ClientId = Env.GetString("CLIENT_ID");
            //ClientSecret = Env.GetString("CLIENT_SECRET");
            ClientId = "9f6e8509f8b34c5bb57f93f3561e705a";
            ClientSecret = "fee3b46e6ef944a494bd49e43b60420d";
        }
        /// <summary>
        /// Creates a query using the clientID and clientSecret to sending user to Spotify to Authorize.
        /// </summary>
        /// <returns></returns>
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
