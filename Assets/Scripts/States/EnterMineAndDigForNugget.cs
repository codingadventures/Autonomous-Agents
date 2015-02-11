using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.States
{
    class EnterMineAndDigForNugget<T> : State<T> where T : Miner
    {
        public EnterMineAndDigForNugget()
        {
            Enter += EnterMineAndDigForNugget_Enter;
            Exit += EnterMineAndDigForNugget_Exit;
        }

        void EnterMineAndDigForNugget_Exit(object sender, AgentEventArgs<T> e)
        {
            throw new NotImplementedException();
        }

        void EnterMineAndDigForNugget_Enter(object sender, AgentEventArgs<T> e)
        {
            throw new NotImplementedException();
        }


        public override void Execute(T agent)
        { 

        }
    }
}
