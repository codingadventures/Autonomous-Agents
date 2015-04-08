

namespace Assets.Scripts.States
{
    using Scripts;
    using Agents;

    public sealed class VisitBankAndDepositGold<T> : State where T : Miner
    {

        private VisitBankAndDepositGold()
        {
            Exit += VisitBankAndDepositGold_Exit;
            Enter += VisitBankAndDepositGold_Enter;
        }



        #region [ Singleton Implementation ]
        public static VisitBankAndDepositGold<T> Instance { get { return Nested.instance; } }

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

            internal static readonly VisitBankAndDepositGold<T> instance = new VisitBankAndDepositGold<T>();
        }
        #endregion


        private void VisitBankAndDepositGold_Enter(object sender, AgentEventArgs<Agent> e)
        {
            e.Agent.TargetLocation = LocationType.Bank;

            if (e.Agent.Location != LocationType.Bank)
                e.Agent.ChangeState<T>(WalkingTo<T>.Instance);
        }

        void VisitBankAndDepositGold_Exit(object sender, AgentEventArgs<Agent> e)
        {
            if (e.Agent.Location == LocationType.Bank)
                e.Agent.Say("Leavin' the Bank");
        }

        #region implemented abstract members of State

        public override void Execute(Agent agent)
        {
            var miner = (T)agent;
            miner.DepositMoney(miner.GoldCarried);

            agent.Say("Depositing gold. Total savings now: " + miner.MoneyInBank);

            if (miner.IsRich())
            {
                agent.Say("WooHoo! Rich enough for now. Back home to mah li'lle lady");
                agent.ChangeState<T>(SleepTillRested<T>.Instance);
            }
            else
            {
                agent.ChangeState<T>(EnterMineAndDigForNugget<T>.Instance);
            }
        }

        #endregion

    }
}

