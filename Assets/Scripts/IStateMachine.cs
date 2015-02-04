using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public interface IStateMachine<T>  
    {

        IState<T> CurrentState { get; }

        IState<T> GlobalState { get; }// { get; protected set; }

        IState<T> PreviousState { get; }// { get; protected set; }

        T Agent { get; }// { get; protected set; }

        void ChangeState(IState<T> newState);
    }
}
