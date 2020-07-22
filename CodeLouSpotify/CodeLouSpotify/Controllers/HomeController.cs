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
        SpotifyToken _token;
        Profile userProfile = new Profile();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        
        //public IActionResult Index()
        //{

        //    return View();
        //}
        
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
          
           if(usertoken.Expiration <= 0)
            {
                GetRefreshToken(usertoken);
            }
         
            return View(usertoken);
        }

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
        /// Takes code recieved from spotify and request a token redirecting the user to the home page if successful. 
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
                    _token = JsonConvert.DeserializeObject<SpotifyToken>(responseString);
                }
                else
                {
                    ViewBag.NotAbleToSignIn = "User not logged in...";
                    return View();
                }


            }
            
            return RedirectToAction("Index", _token);
        }

        [HttpGet]//not sure if I need this
        public async Task<IActionResult> Profile(SpotifyToken userToken)
        {
           //TODO: Fix Profile ActionMethod
           if(userToken.AccessToken== null)
            {
                return RedirectToAction("Callback");
            }
           
            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",userToken.AccessToken);
                var task = await client.GetAsync("https://api.spotify.com/v1/me");
              
                    
                    if (task.IsSuccessStatusCode)
                    {
                        var jsonString =await task.Content.ReadAsStringAsync();
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
