using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TacminMusavir
{
    public class EvrakIndirmeIslem : Hub
    {
        public void EvrakIndir(string mesaj)
        {
            Clients.All.sendMessage(mesaj);
        }
    }
}