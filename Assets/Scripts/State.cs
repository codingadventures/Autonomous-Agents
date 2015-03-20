namespace Assets.Scripts
{
    using Agents;
    using System;

    public abstract class State : IState
    {

        public event EventHandler<AgentEventArgs<Agent>> Enter;
        public event EventHandler<AgentEventArgs<Agent>> Exit; 


        public virtual void OnEnter(AgentEventArgs<Agent> aea)
        {
            var handler = Enter;
            if (handler != null)
            {
                handler(this, aea);
            }
        }

        public virtual void OnExit(AgentEventArgs<Agent> aea)
        {
            var handler = Exit;
            if (handler != null)
            {
                handler(this, aea);
            }
        }
         

        public abstract void Execute(Agent agent);


    }
}