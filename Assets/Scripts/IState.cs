using Assets.Scripts.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public interface IState<T>
    {
        event EventHandler<AgentEventArgs<T>> Enter;
        event EventHandler<AgentEventArgs<T>> Exit;
        event EventHandler<MessageEventArgs<T>> Message;
        
        void OnEnter(AgentEventArgs<T> aea);

        void OnExit(AgentEventArgs<T> aea);

        void OnMessage(MessageEventArgs<T> mea);

        void Execute(T agent);
    }
}
