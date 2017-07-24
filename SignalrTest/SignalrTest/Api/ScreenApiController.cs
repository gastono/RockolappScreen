using Microsoft.AspNet.SignalR;
using Rocolapp.DAL;
using SignalrTest.DAL;
using SignalrTest.Models;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SignalrTest.Api
{
    public class ScreenApiController : ApiController
    {
        RocolappDataAccess dataAccess = new RocolappDataAccess();

        // POST: api/Register
        /// <summary>
        /// Register a new screen for a specific user
        /// </summary>
        /// <param name="newScreen">Information about the new screen to be added to a rockolapp user</param>
        [HttpPost]
        public int Register([FromBody]Screen newScreen)
        {

            var connectionId = dataAccess.RegisterScreen(newScreen.ScreenCode, newScreen.SpotifyUserId);

            var screenHubcontext = GlobalHost.ConnectionManager.GetHubContext<ScrennPusher>();

            if (connectionId!= string.Empty)
            {
                screenHubcontext.Clients.Client(connectionId).GetRockolaInformation(newScreen.SpotifyUserId);

            }

            return 1;
        }

        /// <summary>
        /// Firt screen registration
        /// </summary>
        /// <param name="newScreen">screen info</param>
        [HttpPost]
        public void SaveRockolappScreen([FromBody]Screen newScreen)
        {
            dataAccess.SaveConnectionIdScreenCode(newScreen.ConnectionId, newScreen.ScreenCode);
        }

        [HttpPost]
        public void RemoveScreen([FromBody]Screen newScreen)
        {
            dataAccess.RemoveScreenByConnectionId(newScreen.ConnectionId);
        }

        /// <summary>
        /// Get the current track based on the spotify user id
        /// </summary>
        /// <param name="spotifyUserId">spotify user id</param>
        /// <returns></returns>
        [HttpGet]
        public RockolappScreenData GetCurrentTrack(string spotifyUserId)
        {
            string token;

            var user = dataAccess.GetUser(spotifyUserId);

            var spotify = new SpotifyWebAPI()
            {
                TokenType = "Bearer",
                AccessToken = user.Token,
                UseAuth = true
            };

            PlaybackContext context = spotify.GetPlayingTrack();
          

            if (context.HasError() && context.Error.Status == 401)
            {
                AutorizationCodeAuth auth = new AutorizationCodeAuth();

                //Datos de mi aplicacion Rocolapp
                auth.ClientId = DataAccess.GetRocolappData().ClientId;
                auth.RedirectUri = DataAccess.GetRocolappData().RedirectUri;
                string clientSecret = DataAccess.GetRocolappData().ClientSecret;

                Token newToken = auth.RefreshToken(user.RefreshToken, DataAccess.GetRocolappData().ClientSecret);


                dataAccess.UpdateToken(user.Id.ToString(), newToken.AccessToken);

                token = newToken.AccessToken;

                var withRefreshedToken = new SpotifyWebAPI()
                {
                    TokenType = "Bearer",
                    AccessToken = newToken.AccessToken,
                    UseAuth = true
                };

                context = withRefreshedToken.GetPlayingTrack();

            }

            RockolappScreenData screenData = new RockolappScreenData();

            try
            {


                screenData.TrackName = context.Item.Name;
                screenData.Artist = context.Item.Artists.First().Name;
                screenData.Duration = context.Item.DurationMs;
                screenData.LapsedTime = context.ProgressMs;
                screenData.ImageUrl = context.Item.Album.Images.First().Url;


            }
            catch (Exception)
            {


            }

            return screenData;
        }

        //tiene que haber una pagina por rockola y que cuando se redirija desde el empty screen se vaya directamente a la pagina correspondiente



    }
}
