using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Message
{
    public class MessageEventArgs<T> : EventArgs
    {
        public Telegram Telegram { get; set; }
        public T Agent { get; set; }

        public MessageEventArgs(T agent,Telegram telegram)
        {
            Telegram = telegram;
            Agent = agent;
        }
    }
}
