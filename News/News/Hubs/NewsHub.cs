using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Core.Entity;
using Microsoft.AspNet.SignalR;
namespace News.Hubs
{
    public class NewsHub:Hub
    {
        public void Send(NewsItem model)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(model);
        }
    }
}