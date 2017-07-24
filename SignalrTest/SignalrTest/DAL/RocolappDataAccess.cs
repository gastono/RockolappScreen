using SignalrTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalrTest.DAL
{
    public class RocolappDataAccess
    {
        private RocolappContext context = new RocolappContext();

        public RocolappUser GetUser(string spotifyId)
        {
            try
            {
                var result = context.RocolappUser.Where(x => x.SpotifyId.Equals(spotifyId)).FirstOrDefault();

                return result;

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public RocolappUser UpdateToken(string rocolappId, string token)
        {
            try
            {
                var result = context.RocolappUser.FirstOrDefault(x => x.Id.ToString().Equals(rocolappId));

                result.Token = token;

                context.SaveChanges();

                return result;

            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public string RegisterScreen(string screenCode, string spotifyUserId)
        {
            string connectionId = string.Empty;
            try
            {
                var result = context.Screen.FirstOrDefault(x => x.ScreenCode.Equals(screenCode));

                if (result != null)
                {

                    result.SpotifyUserId = spotifyUserId;
                    result.Connected = true;

                    context.SaveChanges();

                    connectionId = result.ConnectionId;
                }

                return connectionId;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void SaveConnectionIdScreenCode(string connectionId, string screenCode)
        {
            try
            {
                Screen screen = new Screen();

                screen.Connected = false;
                screen.ConnectionId = connectionId;
                screen.ScreenCode = screenCode;

                context.Screen.Add(screen);

                context.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void RemoveScreenByConnectionId(string connectionId)
        {
            try
            {
                var screen = context.Screen.FirstOrDefault(x => x.ConnectionId.Equals(connectionId));

                if (screen != null)
                {
                    context.Screen.Remove(screen);

                    context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool CheckIfScreenExists(int screenCode)
        {
            try
            {
                var result = context.Screen.FirstOrDefault(x => x.ScreenCode.Equals(screenCode.ToString()));

                return result != null;
            }
            catch (Exception e)
            {

                throw;
            }
        }

    }
}