
using UnityEngine;

namespace Assets.Scripts.States
{
	using System;
	using Assets.Scripts;	

	public class GoHomeAndSleepTillRested<T> :  State<T> where T : Miner
	{
		private GoHomeAndSleepTillRested()
		{
			Enter += GoHomeAndSleepTilRested_Enter;
			Exit  += GoHomeAndSleepTilRested_Exit;
		}

		#region [ Singleton Implementation ]
		private static GoHomeAndSleepTillRested<T> instance = null;
		
		
		public static GoHomeAndSleepTillRested<T> Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new GoHomeAndSleepTillRested<T>();
				}
				return instance;
			}
		}
 	 	#endregion

		void GoHomeAndSleepTilRested_Exit(object sender, AgentEventArgs<T> e)
        {
			 
        }

		void GoHomeAndSleepTilRested_Enter(object sender, AgentEventArgs<T> e)
        {
			Debug.Log("Walkin' Home");
			Debug.Log(e.Agent.ToString());
			e.Agent.ChangeLocation(LocationType.HomeSweetHome);
        }


        public override void Execute(T agent)
        { 
			if (!agent.IsTired())
			{
				Debug.Log("All mah fatigue has drained away. Time to find more gold!");
				Debug.Log(agent.ToString());
				
				agent.ChangeState(EnterMineAndDigForNugget<T>.Instance);
			}
			else
			{
				agent.Rest();
				Debug.Log("ZZZZZ....");
				Debug.Log(agent.ToString());
				
			}
        }
	}
}

