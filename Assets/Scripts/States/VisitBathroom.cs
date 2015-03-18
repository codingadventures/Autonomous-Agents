 
 
namespace Assets.Scripts.States
{
    using Agents;
    using UnityEngine;

    public sealed class VisitBathroom<T>: State<T> where T : Elsa
    {
         
        #region [ Singleton Implementation ]

        private VisitBathroom()
        {
            Enter += VisitBathroom_Enter;
            Exit += VisitBathroom_Exit;
        }
         
        public static VisitBathroom<T> Instance { get { return Nested.instance; } }

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

            internal static readonly VisitBathroom<T> instance = new VisitBathroom<T>();
        }
        #endregion

        private void VisitBathroom_Enter(object sender, AgentEventArgs<T> e)
        {
            Debug.Log("Walkin' to the can. Need to powda mah pretty li'lle nose");
        }

        private void VisitBathroom_Exit(object sender, AgentEventArgs<T> e)
        {
            Debug.Log("Leavin' the Jon");
        }

        public override void Execute(T agent)
        {
            Debug.Log("Ahhhhhh! Sweet relief!");
            agent.StateMachine.RevertToPreviousState();  // this completes the state blip
        }
    }
}
