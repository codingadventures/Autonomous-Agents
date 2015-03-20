
using Assets.Scripts.Agents;

namespace Assets.Scripts.Message
{
    using System;

    public class MessageEventArgs<T> : EventArgs where T : Agent
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
