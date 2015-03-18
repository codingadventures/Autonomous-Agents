using System;

namespace Assets.Scripts
{
    public interface IState<T>
    {
        event EventHandler<AgentEventArgs<T>> Enter;
        event EventHandler<AgentEventArgs<T>> Exit;
        event EventHandler<AgentEventArgs<T>> Message;
        
        void OnEnter(AgentEventArgs<T> aea);

        void OnExit(AgentEventArgs<T> aea);

        void OnMessage(AgentEventArgs<T> mea);

        void Execute(T agent);
    }
}
