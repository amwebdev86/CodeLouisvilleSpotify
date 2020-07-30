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
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Policy;

namespace CodeLouSpotify.Controllers
{
    public class HomeController : Controller
    {
        [BindProperty]
        SpotifyUser _user { get; set; } = new SpotifyUser();
        [BindProperty]
        public SpotifyToken SpotifyToken { get; set; }
        [BindProperty]
        Profile userProfile { get; set; } = new Profile();
        [BindProperty]
        public AudioAnalysis TrackAudioAnalysis { get; set; }

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        #region Authentication
        [HttpGet]
        public IActionResult Index()
        {
            //Will be null upon initial entry. once Authenticate is clicked cookie will store the AccessToken.
            var spotifyToken = new SpotifyToken();
            if (string.IsNullOrEmpty(spotifyToken.AccessToken))
            {
                if (Request.Cookies.ContainsKey("Spotify"))
                {
                    spotifyToken.AccessToken = Request.Cookies["Spotify"];
                }
            }


            return View(spotifyToken);
        }

        /// <summary>
        /// Gets required Code to exchange for a Token
        /// </summary>
        /// <returns>Url with code redirecting to /callback</returns>
        public IActionResult AuthorizeUser()
        {
            return Redirect(_user.Authorize());
        }
        /// <summary>
        /// Landing page after recieving code from Spotify. Stores AccessToken in cookie. If Error then the page will show user not logged in 
        /// otherwise returns user to Index.
        /// </summary>
        /// <param name="code"></param>
        /// <returns>Returns user to Index with a stored Access Token Cookie</returns>
        [Route("/callback")]
        public IActionResult Callback(string code)
        {
            SpotifyToken = GetSpotifyToken(code);
            if (SpotifyToken.AccessToken == null)
            {
                ViewBag.NotAbleToSignIn = "User not logged in...";
                return View();
            }
            //Take Access Token from new SpotifyToken and store it in a cookie for future reference.
            Response.Cookies.Append("Spotify", SpotifyToken.AccessToken);

            return RedirectToAction("Index");
        }
        #endregion
        #region  After Authorized Pages
        public IActionResult Home()
        {
            return View();
        }
        #endregion


        #region Tokens
        /// <summary>
        /// Takes code recieved from spotify and requests a Token
        /// </summary>
        /// <param name="code">code returned from spotify</param>
        /// <returns>SpotifyToken</returns>
        public SpotifyToken GetSpotifyToken(string code)
        {
            SpotifyToken = new SpotifyToken(); //debug testing
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
                    //SpotifyToken = newToken;


                }

            }
            //Debug testing.
            SpotifyToken.AccessToken = newToken.AccessToken;
            SpotifyToken.Expiration = newToken.Expiration;
            SpotifyToken.RefreshToken = newToken.RefreshToken;
            SpotifyToken.Scope = newToken.Scope;
            SpotifyToken.TokenType = newToken.TokenType;
            return SpotifyToken;
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
        #endregion

        #region Searching

        public IActionResult Search()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Search(string title, string limit)
        {

            title = Request.Form["title"][0];
            limit = Request.Form["limit"][0];


            return RedirectToAction("SearchResult", new
            {
                title,
                limit,

            });
        }

        [HttpGet]
        public IActionResult SearchResult(string title, string limit)
        {
            if (string.IsNullOrWhiteSpace(limit))
            {
                limit = "1";
            }
            var spotifyToken = new SpotifyToken();
            if (Request.Cookies.ContainsKey("Spotify"))
            {
                spotifyToken.AccessToken = Request.Cookies["Spotify"];
            }
            var track = GetSearchResults(title, limit, spotifyToken.AccessToken).Result;
            return View(track);
        }

        public async Task<Tracks> GetSearchResults(string title, string limit, string userToken)
        {
            var result = new Tracks();
            var formattedTitle = title.Replace(' ', '+');
            string endpoint = $@"https://api.spotify.com/v1/search?q={formattedTitle}&type=track&market=US&limit={limit}";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var task = await client.GetAsync(endpoint);
                if (task.IsSuccessStatusCode)
                {
                    var responseMessage = await task.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<SearchResultModel>(responseMessage).tracks;
                }
            }
            return result;
        }
        #endregion




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Profile()
        {
            //ViewData["UserToken"] = userToken.AccessToken;
            //if (string.IsNullOrEmpty(userToken.AccessToken))
            //{
            //    return RedirectToAction("Index");
            //}
            var spotifyToken = new SpotifyToken();
            if (Request.Cookies.ContainsKey("Spotify"))
            {
                spotifyToken.AccessToken = Request.Cookies["Spotify"];
            }
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", spotifyToken.AccessToken);
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
        public async Task<AudioAnalysis> GetTrackAnalysis(string id, string userToken)
        {
            var analyizedTrack = new AudioAnalysis();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var task = await client.GetAsync($@" https://api.spotify.com/v1/audio-analysis/{id}");
                try
                {
                    if (task.IsSuccessStatusCode)
                    {
                        var json = await task.Content.ReadAsStringAsync();
                        analyizedTrack = JsonConvert.DeserializeObject<AudioAnalysis>(json);
                        return analyizedTrack;
                    }
                    else
                    {
                        return new AudioAnalysis();
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return new AudioAnalysis();
                }

            }
        }
        public IActionResult AnalyzeTrack(Item2 track)
        {
            Debug.WriteLine(track.id);
            var id = track.id;
            var user = Request.Cookies["Spotify"];
            TrackAudioAnalysis = GetTrackAnalysis(id, user).Result;

            return View(TrackAudioAnalysis);
        }

    }
}
