 
using System;

namespace Assets.Scripts
{
    public class AgentEventArgs<T> : EventArgs  
    {
        public T Agent { get; set; }

        public AgentEventArgs(T agent)
        {
            Agent = agent;
        }
    }
}

