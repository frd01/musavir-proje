using System;
using Tacmin.Core.Extensions;
using Tacmin.Core.Model;

namespace Tacmin.Core.Infrastructure
{
    public static class NotifyDeveloper
    {
        public static void Notify(string message)
        {

            foreach (var messagechunkchars in message.ChunkBy(1950))
            {
                var messagechunk = new String(messagechunkchars.ToArray());
                var discoardMessage = new DiscordNotificationModel
                {
                    username = "musavirapp",
                    avatar_url = "https://tacminyazilim.com/assets/images/logo-80x80.png",
                    content = "```ml\n" + messagechunk + "\n```",
                };

                /* using (var client = new WebClient())
                 {
                     var postUrl = $"https://discordapp.com/api/webhooks/873576844081913876/O1rXRUS6ynJwt_A6ET-K_SFwfH70_fZHij5k_iqwYM300TfSuMEuN7SDzRWmg-4Mb5zV";
                     client.Headers["Content-Type"] = "application/json";
                     client.Encoding = Encoding.UTF8;
                     var jsondata = JsonConvert.SerializeObject(discoardMessage);
                     client.UploadString(postUrl, WebRequestMethods.Http.Post, jsondata);
                 }*/
            }
        }
    }
}
