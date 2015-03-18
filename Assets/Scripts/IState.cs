
using Assets.Scripts.Agents;

namespace Assets.Scripts
{
    using System;
    using Message;


    public interface IState<T>
    {
        event EventHandler<AgentEventArgs<T>> Enter;
        event EventHandler<AgentEventArgs<T>> Exit;
        event EventHandler<MessageEventArgs<T>> Message;
        
        void OnEnter(AgentEventArgs<T> aea);

        void OnExit(AgentEventArgs<T> aea);

        void OnMessage(MessageEventArgs<T> telegram);

        void Execute(T agent);
    }
}
