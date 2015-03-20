

using System;
using Assets.Scripts.Agents;

namespace Assets.Scripts
{
    public class StateMachine : IStateMachine  
    {

        public IState  CurrentState { get; set; }


        public IState  GlobalState { get; set; }


        public IState PreviousState { get; set; }


        public Agent Agent { get; set; }

        public StateMachine(Agent agent)
        {
            Agent = agent;
        }

        public void Update()
        {
            if (CurrentState != null) CurrentState.Execute(Agent);

            if (GlobalState != null) GlobalState.Execute(Agent);
        }

        public void RevertToPreviousState()
        {
            ChangeState(PreviousState);
        }


        public void ChangeState(IState  newState)
        {
			if (newState == null)
				throw new ArgumentNullException("newState","The input state cannot be null");

            PreviousState = CurrentState;

			if (CurrentState != null)
            	CurrentState.OnExit(new AgentEventArgs<Agent>(Agent));

            CurrentState = newState;


            CurrentState.OnEnter(new AgentEventArgs<Agent>(Agent));

        }
    }
}
