
namespace Assets.Scripts.States
{
    using Agents;
    using Scripts;
    using UnityEngine;
    public sealed class GoHome<T> : State where T : Miner
    {
 
        private GoHome()
        {
            Enter += GoHome_Enter;
        }



        #region [ Singleton Implementation ]

        public static GoHome<T> Instance { get { return Nested.instance; } }

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

            internal static readonly GoHome<T> instance = new GoHome<T>();
        }
        #endregion
 

        void GoHome_Enter(object sender, AgentEventArgs<Agent> e)
        {
            e.Agent.Say("Walkin' Home");
            e.Agent.ChangeLocation(LocationType.HomeSweetHome);

        }

        public override void Execute(Agent agent)
        {
            var homePosition = agent.LocationManager.Locations[LocationType.HomeSweetHome].position;

            if (Vector3.Distance(homePosition, agent.transform.position) <= 1.0f)
            {
                //We are arrived!!!
                agent.ChangeState<T>(SleepTillRested<T>.Instance);

            }

        }
    }
}

