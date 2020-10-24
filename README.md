# SpotifyApp
Check your user Profile and search for a song and retrieve album information using the Spotify API.
This project was created as a learning experience and is **Not ideal for PRODUCTION**. I will be continually updating this repo. The main purpose of this project was to meet the requirments set forth by [Code Louisville Program](https://codelouisville.org/) . These requirements can be viewed below.
## Development
I created this app using ASP.NET MVC model  to seperate areas of concern as well as avoiding the popular Spotify Library from Nuget for purposes of learning.
I created a HomeController that get/sets objects I created including a 
[SportifyUser](https://github.com/amwebdev86/CodeLouisvilleSpotify/blob/master/CodeLouSpotify/CodeLouSpotify/Data/SpotifyUser.cs) which is responsible for taking the client ID and client secret provided by Spotify and builds and returns a query string that includes the required response code, scope and callback uri required for the spotify app to authorize the application. The SpotifyUser object is instantiated with either no params for the default credentials I hardcoded or you can provide your own ClientID and Secret.
The HomeController then has properties that are binded to the models to provide the information required by various views [HERE](https://github.com/amwebdev86/CodeLouisvilleSpotify/blob/FinalizeBranch/CodeLouSpotify/CodeLouSpotify/Controllers/HomeController.cs) Upon hitting the Index endpoint the HomeController checks for a stored Access code in the Cookie storage if one is available it adds the the token to the current SpotifyUser or creats a new token when user hits authenticate sending them to authorize through Spotify and returning to the callback view with the stored credentials. From there the user is able to search for songs and check their respective profiles using the provided links.
- Token Model.
```C#
namespace CodeLouSpotify.Models
{

    public class SpotifyToken
    {
        [JsonProperty(PropertyName="access_token")]
        public string AccessToken { get; set; }
        [JsonProperty(PropertyName="token_type")]
        public string TokenType { get; set; }
        [JsonProperty(PropertyName="scope")]
        public string Scope { get; set; }
        [JsonProperty(PropertyName="expires_in")]
        public int Expiration { get; set; }
        [JsonProperty(PropertyName="refresh_token")]
        public string RefreshToken { get; set; }
    }

}
```
- Example of the callback view used:
```C#
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
```
The HomeController also refreshes the token if needed:
```C#
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
```
## Key decicisons include:
- Using HTTPClient to make aysnc GET request and deserialize JSON returned into models. 
[CODE](https://github.com/amwebdev86/CodeLouisvilleSpotify/blob/b47a7ca35d33c645937fccebf7b7d0d16c8e21be/CodeLouSpotify/CodeLouSpotify/Controllers/HomeController.cs#L217)
```C#
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
```
-using CookieStorage
- Created Models to digest the returned json ex: [SearchResult model](https://github.com/amwebdev86/CodeLouisvilleSpotify/blob/FinalizeBranch/CodeLouSpotify/CodeLouSpotify/Models/SearchResultModel.cs)


### Thoughts
I have hit some pitfalls using this approach, one of which was persiting the data between views. I worked around this issue using CookieStorage. 
I have a long HomeController and can probably seperate areas of concern and make the code more readable and remove single point of failure. I will be doing this in a future update. I can remove a lot of overhead by using a nuget package to handle my authentication, again I avoided this approach for learning purposes. I look forward to continuing this project after evaluation and refactor it to be deployed.



### Steps 
1. Download the zip file or fork the project
2. unzip the files and open the CodeLouisvilleSpotify folder 
3. Double click the .sln file called 'CodeLouSpotify'
4. You will need to use LocalHost port 44363 for access.
5. run the application.
If the application will not run it may need a ClientID and Secret since it has been reset for security purposes you can provide this information in the [HomeController](https://github.com/amwebdev86/CodeLouisvilleSpotify/blob/FinalizeBranch/CodeLouSpotify/CodeLouSpotify/Controllers/HomeController.cs) call to instantiate a new SpotifyUser for example:
```C#
  [HttpGet]
        public IActionResult Index()
        {
            //Will be null upon initial entry. once Authenticate is clicked cookie will store the AccessToken.
            var spotifyToken = new SpotifyToken('clientId', 'clientSecret');
            if (string.IsNullOrEmpty(spotifyToken.AccessToken))
            {
                if (Request.Cookies.ContainsKey("Spotify"))
                {
                    spotifyToken.AccessToken = Request.Cookies["Spotify"];
                }
            }


            return View(spotifyToken);
        }
```


## Project outlined requirements (Code Louisville)
This project was created for Code Louisville C# track and the following outlines the min.Requirements for the project. Full requirements can be viewed [HERE](https://docs.google.com/document/d/1sFJskj06VFKwinZg-8822R6O-PRlMF60AGTVGagslJw/edit)
## Min. Requirements met
- Create a class, then create at least one object of that class and populate it with data
- Create a dictionary or list, populate it with several values, retrieve at least one value, and use it in your program
- Connect to an external/3rd party API and read data into your app

