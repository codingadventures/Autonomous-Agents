using System;

namespace Assets.Scripts.States
{
    using Agents;
    using Random = Random;

    public class DoHousework<T> : State where T : Elsa
    {
        private static readonly Random Random = new Random();

        #region [ Singleton Implementation ]

        private DoHousework()
        {
            Enter += DoHousework_Enter;
        }



        public static DoHousework<T> Instance { get { return Nested.instance; } }

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

            internal static readonly DoHousework<T> instance = new DoHousework<T>();
        }
        #endregion

        private void DoHousework_Enter(object sender, AgentEventArgs<Agent> e)
        {
            e.Agent.Say("Time to do some more housework!");
        }

        public override void Execute(Agent agent)
        {
            switch (Random.Next(3))
            {
                case 0:
                    agent.Say("Moppin' the floor");
                    break;
                case 1:
                    agent.Say("Washin' the dishes");
                    break;
                case 2:
                    agent.Say("Makin' the bed");
                    break;
            }
        }
    }
}
