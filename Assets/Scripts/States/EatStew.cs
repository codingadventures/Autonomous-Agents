
namespace Assets.Scripts.States
{
    using Agents;

    public class EatStew<T> : State where T : Miner
    {

        #region [ Singleton Implementation ]

        private EatStew()
        {
            Enter += EatStew_Enter;
            Exit += EatStew_Exit;
        }

      
        public static EatStew<T> Instance { get { return Nested.instance; } }

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

            internal static readonly EatStew<T> instance = new EatStew<T>();
        }
        #endregion

        void EatStew_Enter(object sender, AgentEventArgs<Agent> e)
        {
            e.Agent.Say("Smells Reaaal goood Elsa!");
        }

        private void EatStew_Exit(object sender, AgentEventArgs<Agent> e)
        {
            e.Agent.Say("Thankya li'lle lady. Ah better get back to whatever ah wuz doin'");
        }
         

        public override void Execute(Agent agent)
        {
            agent.Say("Tastes real good too!");
            agent.StateMachine.RevertToPreviousState();
        }
    }
}
