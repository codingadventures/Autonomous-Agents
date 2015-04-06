
namespace Assets.Scripts.States
{
    using Agents;
    using Message;
    using Scripts;
    using UnityEngine;
    public sealed class GoHomeAndSleepTillRested<T> : State where T : Miner
    {
        private GoHomeAndSleepTillRested()
        {
            Enter += GoHomeAndSleepTilRested_Enter;
            Exit += GoHomeAndSleepTilRested_Exit;
        }



        #region [ Singleton Implementation ]

        public static GoHomeAndSleepTillRested<T> Instance { get { return Nested.instance; } }

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

            internal static readonly GoHomeAndSleepTillRested<T> instance = new GoHomeAndSleepTillRested<T>();
        }
        #endregion

        void GoHomeAndSleepTilRested_Exit(object sender, AgentEventArgs<Agent> e)
        {

        }

        void GoHomeAndSleepTilRested_Enter(object sender, AgentEventArgs<Agent> e)
        {
            e.Agent.Say("Walkin' Home");
            e.Agent.ChangeLocation(LocationType.HomeSweetHome);
            try
            {
                Messenger.Broadcast(MessageType.HiHoneyImHome.ToString(), 
                    new MessageEventArgs<Agent>(e.Agent, 
                        new Telegram { MessageType = MessageType.HiHoneyImHome }));

            }
            catch (Messenger.BroadcastException be)
            {
                Debug.LogException(be);

            }
        }

        public override void Execute(Agent agent)
        {
            var miner = (T)agent;
            if (!miner.IsTired())
            {
                agent.Say("All mah fatigue has drained away. Time to find more gold!");

                agent.ChangeState<T>(EnterMineAndDigForNugget<T>.Instance);
            }
            else
            {
                miner.Rest();
                agent.Say("ZZZZZ....");
            }
        }
    }
}

