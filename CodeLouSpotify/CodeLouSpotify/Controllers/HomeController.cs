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
        SpotifyUser _user = new SpotifyUser();
        SpotifyToken _token = new SpotifyToken();
        Profile userProfile = new Profile();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //TODO: HOME PAGE AFTER LOGGIN
        public IActionResult Home(SpotifyToken userToken)
        {
            if (userToken.Expiration <= 0)
            {
                GetRefreshToken(userToken);
            }

            return View();
        }
        [HttpGet]
        public IActionResult Search([FromQuery]SpotifyToken spotifyToken)
        {

            return View();
        }
        [HttpPost]
        public IActionResult Search()
        {
           var title = Request.Form["title"][0];
           var  limit = Request.Form["limit"][0];
            return RedirectToAction("SearchResult", new
            {
                title,
                limit
            });


        }
        public IActionResult SearchResult()
        {

            return View();
        }


        /// <summary>
        /// Refreshes user token.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>

        public IActionResult GetRefreshToken(SpotifyToken token)
        {
            var responseString = string.Empty;

            //code
            if (token == null)
            {
                return Redirect(_user.Authorize());
            }
            using (HttpClient refreshClient = new HttpClient())
            {
                refreshClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(_user.ClientId + ":" + _user.ClientSecret)));
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

        public IActionResult Index(SpotifyToken usertoken)
        {

            if (usertoken.Expiration <= 0)
            {
                GetRefreshToken(usertoken);
            }

            return View(usertoken);
        }
        //TODO: [Update, Convience] Pass ClientID and Seceret as a Query Param from input for Code Lou to login without needing email.
        public IActionResult AuthorizeUser()
        {
            return Redirect(_user.Authorize());
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
        /// Returns IActionResult Redirecting user to the Index page with the Code recieved from spotify.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
       
        [Route("/callback")]
        public IActionResult Callback(string code)
        {
            _token = GetSpotifyToken(code);
            ViewBag.NotAbleToSignIn = "User not logged in...";

            
            return RedirectToAction("Index", _token);
        }

        /// <summary>
        /// Takes code recieved from spotify and requests a Token
        /// </summary>
        /// <param name="code">code returned from spotify</param>
        /// <returns>SpotifyToken</returns>
        public SpotifyToken GetSpotifyToken(string code)
        {//TODO: [Update, Want] store Token in Runtime cache for access and refreshing. 
            SpotifyToken newToken = new SpotifyToken();
            string responseString = string.Empty;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(_user.ClientId + ":" + _user.ClientSecret)));
                FormUrlEncodedContent formContent = new FormUrlEncodedContent(
                        new[]
                        {
                            new KeyValuePair<string,string>("code", code),
                            new KeyValuePair<string,string>("redirect_uri", _user.CallbackUri),
                            new KeyValuePair<string, string>("grant_type", "authorization_code")
                        });
                var response = client.PostAsync("https://accounts.spotify.com/api/token", formContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    responseString = responseContent.ReadAsStringAsync().Result;
                    newToken = JsonConvert.DeserializeObject<SpotifyToken>(responseString);
                }
            }
            return newToken;
        }

        [HttpGet]//not sure if I need this aatribute
        public async Task<IActionResult> Profile([FromQuery]SpotifyToken userToken)
        {
            ViewData["UserToken"] = userToken.AccessToken;
            if (string.IsNullOrEmpty(userToken.AccessToken))
            {
                return RedirectToAction("Index");
            }

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken.AccessToken);
                var task = await client.GetAsync("https://api.spotify.com/v1/me");


                if (task.IsSuccessStatusCode)
                {
                    var jsonString = await task.Content.ReadAsStringAsync();
                    userProfile = JsonConvert.DeserializeObject<Profile>(jsonString);
                }
                else
                {
                    ViewBag.NotSuccessful = task.StatusCode.ToString();
                    return View(userProfile);
                }

            }


            return View(userProfile);
        }
    }
}
