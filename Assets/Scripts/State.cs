using System;

namespace Assets.Scripts
{

    public abstract class State<T> : IState<T>
    {
       


        public event EventHandler<AgentEventArgs<T>> Enter;
        public event EventHandler<AgentEventArgs<T>> Exit;
        public event EventHandler<AgentEventArgs<T>> Message;


        public virtual void OnEnter(AgentEventArgs<T> aea)
        {
            var handler = Enter;
            if (handler != null)
            {
                handler(this, aea);
            }
        }

        public virtual void OnExit(AgentEventArgs<T> aea)
        {
            var handler = Exit;
            if (handler != null)
            {
                handler(this, aea);
            }
        }

        public void OnMessage(AgentEventArgs<T> aea)
        {
            var handler = Message;
            if (handler != null)
            {
                handler(this, aea);
            }
        }

		
		public abstract void Execute(T agent);


    }
}