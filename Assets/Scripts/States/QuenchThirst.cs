namespace Assets.Scripts.States
{
    using Agents;

    public sealed class QuenchThirst<T> : State  where T : Miner
	{
 
		private QuenchThirst ()
		{ 
			Exit  += QuenchThirst_Exit;
		}

		#region [ Singleton Implementation ]

		public static QuenchThirst<T> Instance { get { return Nested.instance; } }
		
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
			
			internal static readonly QuenchThirst<T> instance = new QuenchThirst<T>();
		}
		#endregion

		 

        void QuenchThirst_Exit(object sender, AgentEventArgs<Agent> e)
		{
            e.Agent.Say("Leaving the saloon, feelin' good");
		}

		#region implemented abstract members of State

		public override void Execute (Agent agent)
		{
		    var miner = (T) agent;
            miner.Drink();
            miner.SpendMoney(2);
			agent.Say("That's mighty fine sippin' liquer");
			agent.ChangeState<T>(EnterMineAndDigForNugget<T>.Instance);
		}

		#endregion

	}
}

