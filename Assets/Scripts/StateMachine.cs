﻿using System;
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
            if (CurrentState != null) CurrentState.Execute(Agent);

            if (GlobalState != null) GlobalState.Execute(Agent);
        }
        

        public void ChangeState(IState<T> newState)
        {

            PreviousState = CurrentState;

            CurrentState.OnExit(new AgentEventArgs<T>(Agent));

            CurrentState = newState;

            CurrentState.OnEnter(new AgentEventArgs<T>(Agent));

        }
    }
}
