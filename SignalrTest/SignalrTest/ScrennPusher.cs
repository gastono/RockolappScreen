using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalrTest
{
    public class ScrennPusher : Hub
    {
       /// <summary>
       /// Prompt screen to load information about an specific rockolapp user
       /// </summary>
       /// <param name="connectionId">Screen Id</param>
       /// <param name="rockolappUser">Rockolapp user id</param>
        public void PromptScreen(string connectionId, string rockolappUser)
        {
            Clients.Client(connectionId).GetRockolaInformation(rockolappUser);
        }

        


    }
}