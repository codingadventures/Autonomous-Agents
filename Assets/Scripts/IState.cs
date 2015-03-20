
using Assets.Scripts.Agents;

namespace Assets.Scripts
{
    using System;


    public interface IState 
    {
        event EventHandler<AgentEventArgs<Agent>> Enter;
        event EventHandler<AgentEventArgs<Agent>> Exit;

        void OnEnter(AgentEventArgs<Agent> aea);

        void OnExit(AgentEventArgs<Agent> aea);
          
        void Execute(Agent agent);
    }
     
}
