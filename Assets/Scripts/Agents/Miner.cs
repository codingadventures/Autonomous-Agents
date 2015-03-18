using System.Collections;
using Assets.Scripts.Message;
using Assets.Scripts.States;
using UnityEngine;

namespace Assets.Scripts.Agents
{ 
    public class Miner : MonoBehaviour, IAgent<Miner>
    {
		#region [ Public Field - Used By Unity Interface ]

		public int MaxThirst;

		public int MaxFatigue;

		public int MaxGoldCarried;

		public int RichnessThreshold;

		public float UpdateStep;

		#endregion

         
		#region[ Public Properties ]
		public int GoldCarried	{get; private set;}

		public int MoneyInBank	{get; private set;}

		public int Thirst	{get; private set;}

		public int Fatigue	{get; private set;}

		public LocationType Location {get; private set;}
       
		public int NextValidId { get; set; }
		
		public IStateMachine<Miner> StateMachine { get; set; }
        #endregion

		#region [ Unity Monobehavior Events ]


        void Awake()
        {
            NextValidId = Random.Range(0, 100);
            StateMachine = new StateMachine<Miner>(this);
        }

        // Use this for initialization
        void Start()
        {
			StateMachine.ChangeState(GoHomeAndSleepTillRested<Miner>.Instance);
			StateMachine.GlobalState = MinerGlobalState<Miner>.Instance; 

			StartCoroutine(PerformUpdate());
        }


		IEnumerator PerformUpdate()
		{
			while(true)
			{
            	Thirst += 1;

            	StateMachine.Update();

				yield return new WaitForSeconds(UpdateStep);
			}
        }

		#endregion

		#region [ Overrides ]

		public override string ToString()
		{
			return string.Format ("[Miner - {5}: GoldCarried={0}, MoneyInBank={1}, Thirst={2}, Fatigue={3}, LocationType={4}]\n", GoldCarried, MoneyInBank, Thirst, Fatigue, Location, NextValidId);
		}

		#endregion

		#region [ Public Methods ]

		public void IncreaseFatigue(int? fatigue = null)
		{
			int fatigueTosum = fatigue ?? 1;

			Fatigue += fatigueTosum;
		}

		public void IncreaseThirst(int? thirst = null)
		{
			int thirstTosum = thirst ?? 1;
			
			Thirst += thirstTosum;
		}

		public void Drink()
		{
			Thirst = 0;
		}

		public void DepositMoney(int money)
		{
			MoneyInBank += money;
			GoldCarried = 0;
		}

		public void SpendMoney(int moneySpent)
		{
			MoneyInBank -= moneySpent;
		}

		public void Rest()
		{
			Fatigue--;
		}

		public void ChangeLocation(LocationType locationType)
		{
			Location = locationType;
		}

		public void AddGoldToInventory(int goldfound)
		{
			GoldCarried += goldfound;
		}

		public bool IsRich()
		{
			return MoneyInBank >= RichnessThreshold;
		}

		public bool IsThirsty()
		{
			return Thirst >= MaxThirst;
		}

		public bool IsPocketFull()
		{
			return GoldCarried >= MaxGoldCarried;
		}

		public bool IsTired()
		{
			return Fatigue >= MaxFatigue;
		}

		public void ChangeState<T>(IState<T> newState)
		{
			StateMachine.ChangeState((IState<Miner>)newState);
		}

		#endregion
    }

}
