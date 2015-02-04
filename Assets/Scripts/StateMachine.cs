using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class StateMachine<T> : IStateMachine<T> 
    {

        public IState<T> CurrentState { get; set; }


        public IState<T> GlobalState { get; set; }


        public IState<T> PreviousState { get; set; }


        public T Agent { get; set; }

        public StateMachine(T agent)
        {
            Agent = agent;
        }

        public void Update()
        {
            //if (CurrentState != null) CurrentState.Execute(agent);

            //if (GlobalState != null) GlobalState.Execute(agent);
        }

        //public void ChangeState(State<T> newState)
        //{
        //}

        public void ChangeState(IState<T> newState)
        {
            
            //_previousState = CurrentState;

            //_currentState.O

            //_currentState = newState;

        }
    }
}
