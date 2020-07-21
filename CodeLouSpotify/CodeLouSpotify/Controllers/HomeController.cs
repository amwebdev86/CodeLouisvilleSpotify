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

namespace CodeLouSpotify.Controllers
{
    public class HomeController : Controller
    {
        SpotifyUser user = new SpotifyUser();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
      

        public IActionResult Index()
        {
            
            return View();
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
        /// 
        /// </summary>
        /// <param name="code">code returned from spotify</param>
        /// <returns></returns>
        [Route("/callback")]
        public IActionResult Callback(string code)
        {
            string responseString = string.Empty;

            if(code == null)
            {
                return NotFound();
            }
            using(HttpClient client = new HttpClient())
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
                var responseContent = response.Content;
                responseString = responseContent.ReadAsStringAsync().Result;

            }
            return View();
        }
    }
}
