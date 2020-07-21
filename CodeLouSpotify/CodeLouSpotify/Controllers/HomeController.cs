using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeLouSpotify.Models;
using Microsoft.AspNetCore.Http.Extensions;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace CodeLouSpotify.Controllers
{
    public class HomeController : Controller
    {
        SpotifyUser user = new SpotifyUser();
        SpotifyToken token = new SpotifyToken();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        //public IActionResult Index()
        //{

        //    return View();
        //}
        //TODO: Refresh Token method 
        public IActionResult GetRefreshToken(SpotifyToken token)
        {
            var responseString = string.Empty;

            //code
            if (token == null)
            {
                return Redirect(user.Authorize());
            }
            using (HttpClient refreshClient = new HttpClient())
            {
                refreshClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(user.ClientId + ":" + user.ClientSecret)));
                FormUrlEncodedContent formContent = new FormUrlEncodedContent(
                    new[]
                        {
                            new KeyValuePair<string,string>("grant_type","refresh_token"),
                            new KeyValuePair<string, string>("refresh_token", token.RefreshToken)
                        }

                    );
                var response = refreshClient.PostAsync("https://accounts.spotify.com/api/token", formContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    responseString = responseContent.ReadAsStringAsync().Result;
                    var newToken = JsonConvert.DeserializeObject<SpotifyToken>(responseString);
                    token.AccessToken = newToken.AccessToken;
                    token.TokenType = newToken.TokenType;
                    token.Expiration = newToken.Expiration;
                }
                return View("Index", token);
            }
        }
        public IActionResult Index(SpotifyToken token)
        {
            if (token.Expiration <= 0)
            {
                //RefreshTokenMethodCall
                Debug.Write("Called Refresh...");
                GetRefreshToken(token);
                return View(token);
            }

            return View(token);
        }
        public IActionResult AuthorizeUser()
        {
            return Redirect(user.Authorize());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        /// <summary>
        /// Returns user to /callback after being authenticated.
        /// </summary>
        /// <param name="code">code returned from spotify</param>
        /// <returns></returns>
        [Route("/callback")]
        public IActionResult Callback(string code)
        {
            string responseString = string.Empty;

            if (code == null)
            {
                return NotFound();
            }
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(user.ClientId + ":" + user.ClientSecret)));
                FormUrlEncodedContent formContent = new FormUrlEncodedContent(
                        new[]
                        {
                            new KeyValuePair<string,string>("code", code),
                            new KeyValuePair<string,string>("redirect_uri", user.CallbackUri),
                            new KeyValuePair<string, string>("grant_type", "authorization_code")
                        });
                var response = client.PostAsync("https://accounts.spotify.com/api/token", formContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    responseString = responseContent.ReadAsStringAsync().Result;
                    token = JsonConvert.DeserializeObject<SpotifyToken>(responseString);

                }
                else
                {
                    ViewBag.NotAbleToSignIn = "User not logged in...";
                }


            }
            
            return View("Index",token);
        }
    }
}
