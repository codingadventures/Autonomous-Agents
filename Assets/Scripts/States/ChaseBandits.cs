using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Agents;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.States
{
    class ChaseBandits<T> : State where T : Sheriff
    {

        #region [ Singleton Implementation ]
        public static ChaseBandits<T> Instance { get { return Nested.instance; } }

        private ChaseBandits()
        {
            Enter += ChaseBandits_Enter;
        }



        /// This is a fully lazy initialization implementation
        /// Instantiation is triggered by the first reference to the static member of the nested class, 
        /// which only occurs in Instance. This means the implementation is fully lazy.
        /// Note that although nested classes have access to the enclosing class's private members, the reverse is not true, 
        /// hence the need for instance to be internal here. That doesn't raise any other problems, though, as the class itself is private. 
        /// The code is a bit more complicated in order to make the instantiation lazy, however.
        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly ChaseBandits<T> instance = new ChaseBandits<T>();
        }
        #endregion
        private void ChaseBandits_Enter(object sender, AgentEventArgs<Agent> e)
        {
            e.Agent.Say("I found you!!!! Now you go to Jail!");
            e.Agent.TargetLocation = LocationType.EscapePoint;

            if (e.Agent.Location != LocationType.EscapePoint)
                e.Agent.ChangeState<T>(WalkingTo<T>.Instance);
        }
        #region implemented abstract members of State

        public override void Execute(Agent agent)
        {

            var target = Object.FindObjectOfType<Bandit>();

            if (target == null)
                throw new Exception("Bandit is null!");

            var position = target.transform.position;

            if (Vector3.Distance(position, agent.transform.position) <= 3.0f)
            {
                agent.Say("Got you!");
                agent.StateMachine.RevertToPreviousState();
            }

            agent.Say("Bandits come here!!!! Bang Bang!!");
        }

        #endregion
    }
}
