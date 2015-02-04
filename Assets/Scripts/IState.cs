using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public interface IState<T> 
    {
         
        void OnEnter(AgentEventArgs<T> e);

         void OnExit(AgentEventArgs<T> e);

    }
}
