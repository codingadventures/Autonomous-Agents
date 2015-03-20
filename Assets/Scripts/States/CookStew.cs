
namespace Assets.Scripts.States
{
    using Agents;
    using Message;

    public class CookStew<T> : State where T : Elsa
    {
        #region [ Singleton Implementation ]

        private CookStew()
        {
            Enter += CookStew_Enter;
            Exit += CookStew_Exit;
        }


        public static CookStew<T> Instance { get { return Nested.instance; } }

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

            internal static readonly CookStew<T> instance = new CookStew<T>();
        }
        #endregion


        private void CookStew_Enter(object sender, AgentEventArgs<Agent> e)
        {
            var wife = (T)e.Agent;
            if (wife.Cooking) return;

            wife.Say("Putting the stew in the oven");

            wife.Cooking = true;
        }

        private void CookStew_Exit(object sender, AgentEventArgs<Agent> e)
        {
            var wife = (T)e.Agent;

            wife.Say("Puttin' the stew on the table");
            wife.Say("StewReady! Lets eat");
            wife.Cooking = false;
            Messenger.Broadcast(MessageType.StewsReady.ToString(),
                new MessageEventArgs<Agent>(e.Agent,new Telegram{MessageType = MessageType.StewsReady})
                );
        }



        public override void Execute(Agent agent)
        {
            agent.Say("Fussin' over food");
            agent.ChangeState<T>(DoHousework<T>.Instance);
        }
    }
}
