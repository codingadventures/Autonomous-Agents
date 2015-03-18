using Assets.Scripts.Message;

namespace Assets.Scripts.States
{
    using System;
    using Agents;
    using UnityEngine;

    public class CookStew<T> : State<T> where T : Elsa
    {



        #region [ Singleton Implementation ]

        private CookStew()
        {
            Enter += CookStew_Enter;
            Exit += CookStew_Exit;
            Message += CookStew_Message;

            Messenger.AddListener<object, MessageEventArgs<T>>("Signals", CookStew_Message);

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


        private void CookStew_Enter(object sender, AgentEventArgs<T> e)
        {
            if (e.Agent.Cooking) return;

            Debug.Log("Putting the stew in the oven");

            //Message.DispatchMessage(2, minersWife.Id, minersWife.Id, MessageType.StewsReady);
            Messenger.Broadcast("StewReady", new Telegram { MessageType = MessageType.StewsReady });
            e.Agent.Cooking = true;
        }

        private void CookStew_Exit(object sender, AgentEventArgs<T> e)
        {
            Debug.Log("Puttin' the stew on the table");
        }

        void CookStew_Message(object sender, MessageEventArgs<T> mea)
        {
            switch (mea.Telegram.MessageType)
            {
                case MessageType.HiHoneyImHome:
                    // Ignored here; handled in WifesGlobalState below
                    return;
                case MessageType.StewsReady:
                    // Tell Miner that the stew is ready now by sending a message with no delay
                    Debug.Log("Message handled by " + mea.Telegram.Receiver + " at time ");
                    Debug.Log("StewReady! Lets eat");
                    //????? why send anoter message?
                    //Messenger.Broadcast("StewReady"MessageType.StewsReady);
                    mea.Agent.Cooking = false;
                    mea.Agent.ChangeState(DoHousework<Elsa>.Instance);

                    break;
            }
        }

        public override void Execute(T agent)
        {
            Debug.Log("Fussin' over food");
        }
    }
}
