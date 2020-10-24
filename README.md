# CodeLouisvilleSpotify
Search for a song and retrieve album information using the spotify API.

I created this app using ASP.NET MVC model to seperate areas of concern. I wanted to avoid importing a library for purposes of learning which lead a few key decisions when coding.
The first was creating an object to store the required Authentication information required to access the api . With this data created I decided to pass the information via Cookie storage to persist the data and pass it to other contorller views.

## Purpose
This project demostrates consuming Spotify web API. It displays Song information based on the user's query. 
### Steps 
1. Download the zip file or fork the project
2. unzip the files and open the CodeLouisvilleSpotify folder 
3. Double click the .sln file called 'CodeLouSpotify'
4. You will need to use LocalHost port 44363 for access.
5. run the application.

NOTE: If you recieve and issue with authenticating I may need to whitelist your URL.
## Authorization Methods
 - Authorization Code

## Min. Requirements met
- Create a class, then create at least one object of that class and populate it with data
- Create a dictionary or list, populate it with several values, retrieve at least one value, and use it in your program
- Connect to an external/3rd party API and read data into your app
## Upcoming
- Artist Search
- Graphs displaying AudioAnalysis data
- Edit playlists
