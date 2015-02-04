using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public interface IAgent<T> 
    {
        IStateMachine<T> StateMachine { get; set; }

        int NextValidId {  get; set; }
    }
}
