
namespace Assets.Scripts.States
{
    using Agents;

    /// <summary>
    /// Enter mine and Dig for Nugget
    /// </summary>
    public sealed class EnterMineAndDigForNugget<T> : State where T : Miner
    {
        private EnterMineAndDigForNugget()
        {
            Exit += EnterMineAndDigForNugget_Exit;
            Enter += EnterMineAndDigForNugget_Enter;
        }




        #region [ Singleton Implementation ]
        public static EnterMineAndDigForNugget<T> Instance { get { return Nested.instance; } }

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

            internal static readonly EnterMineAndDigForNugget<T> instance = new EnterMineAndDigForNugget<T>();
        }
        #endregion


        void EnterMineAndDigForNugget_Enter(object sender, AgentEventArgs<Agent> e)
        {
            e.Agent.TargetLocation = LocationType.Goldmine;

            if (e.Agent.Location != LocationType.Goldmine)
                e.Agent.ChangeState<T>(WalkingTo<T>.Instance);
        }

        void EnterMineAndDigForNugget_Exit(object sender, AgentEventArgs<Agent> e)
        {
            if (e.Agent.Location == LocationType.Goldmine)
                e.Agent.Say("Ah'm leavin' the gold mine with mah pockets full o' sweet gold");
        }

        public override void Execute(Agent agent)
        {
            var miner = (T)agent;
            miner.AddGoldToInventory(1);

            miner.IncreaseFatigue();

            miner.Say("Pickin' up a nugget");

            if (miner.IsPocketFull())
            {
                miner.ChangeState<T>(VisitBankAndDepositGold<T>.Instance);
            }

            if (miner.IsThirsty())
            {
                miner.ChangeState<T>(QuenchThirst<T>.Instance);
            }
        }
    }
}
