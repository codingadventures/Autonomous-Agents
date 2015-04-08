
namespace Assets.Scripts.States
{
    using Agents;
    using Scripts;
    using UnityEngine;
    public sealed class WalkingTo<T> : State where T : Miner
    {

        private WalkingTo()
        {
            Enter += WalkingTo_Enter;
        }



        #region [ Singleton Implementation ]

        public static WalkingTo<T> Instance { get { return Nested.instance; } }

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

            internal static readonly WalkingTo<T> instance = new WalkingTo<T>();
        }
        #endregion


        void WalkingTo_Enter(object sender, AgentEventArgs<Agent> e)
        {
            e.Agent.Say(string.Format("Walkin' to {0}", e.Agent.TargetLocation));
            e.Agent.ChangeLocation(e.Agent.TargetLocation);

        }

        public override void Execute(Agent agent)
        {
            var target = agent.LocationManager.Locations[agent.TargetLocation].position;
            
            target.y = 0;

            if (Vector3.Distance(target, agent.transform.position) <= 3.0f)
            {
                agent.StateMachine.RevertToPreviousState();
            }

        }
    }
}

