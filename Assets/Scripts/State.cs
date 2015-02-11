using UnityEngine;
using System.Collections;
using System;

namespace Assets.Scripts
{

    public abstract class State<T> : IState<T>
    {
        public virtual void Execute(T agent)
        {

        }


        public event EventHandler<AgentEventArgs<T>> Enter;
        public event EventHandler<AgentEventArgs<T>> Exit;


        public virtual void OnEnter(AgentEventArgs<T> e)
        {
            var handler = Enter;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public virtual void OnExit(AgentEventArgs<T> e)
        {
            var handler = Exit;
            if (handler != null)
            {
                handler(this, e);
            }
        }

    }
}