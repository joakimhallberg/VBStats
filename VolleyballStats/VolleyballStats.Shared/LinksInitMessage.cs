using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.Messaging;
using VolleyballStats.Model;

namespace VolleyballStats
{
    public class LinksInitMessage : MessageBase
    {
        public Game Payload { get; set; }
        public Action<Game> Callback { get; set; }

        public LinksInitMessage(Game payload)
        {
            this.Payload = payload;
        }

        public LinksInitMessage(Action<Game> callback)
        {
            this.Callback = callback;
        }
    }

}
