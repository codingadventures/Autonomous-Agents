using System;
using UnityEngine;

namespace Assets.Scripts.States
{
	/// <summary>
	/// Enter mine and Dig for Nugget
	/// </summary>
    public sealed class EnterMineAndDigForNugget<T> : State<T> where T : Miner
    {
		private EnterMineAndDigForNugget()
		{
			Enter += EnterMineAndDigForNugget_Enter;
			Exit += EnterMineAndDigForNugget_Exit;
		}
		 

		#region [ Singleton Implementation ]
		private static EnterMineAndDigForNugget<T> instance = null;
		
		 
		public static EnterMineAndDigForNugget<T> Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new EnterMineAndDigForNugget<T>();
				}
				return instance;
			}
		}
		#endregion

		void EnterMineAndDigForNugget_Enter(object sender, AgentEventArgs<T> e)
        {
            if (e.Agent.Location != LocationType.Goldmine)
			{
				Debug.Log("Walkin' to the goldmine: AgentId" + e.Agent.NextValidId);
				e.Agent.ChangeLocation(LocationType.Goldmine);
			}
        }
       
        void EnterMineAndDigForNugget_Exit(object sender, AgentEventArgs<T> e)
        {
			Debug.Log("Ah'm leavin' the gold mine with mah pockets full o' sweet gold");
        }

        public override void Execute(T agent)
        { 
			agent.AddGoldToInventory(1);

			agent.IncreaseFatigue();

			Debug.Log("Pickin' up a nugget");

			if (agent.IsPocketFull())
			{
				agent.ChangeState(VisitBankAndDepositGold<T>.Instance);
			}

			if (agent.IsThirsty())
			{
				agent.ChangeState(QuenchThirst<T>.Instance);
			}
        }
    }
}
