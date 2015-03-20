

using Assets.Scripts.Message;

namespace Assets.Scripts.Agents
{
    using System.Collections;
    using States;
    using UnityEngine;

    public class Miner : Agent
    {
        #region [ Public Field - Used By Unity Interface ]

        public int MaxThirst;

        public int MaxFatigue;

        public int MaxGoldCarried;

        public int RichnessThreshold;

        #endregion

        #region[ Public Properties ]
        public int GoldCarried { get; private set; }

        public int MoneyInBank { get; private set; }

        public int Thirst { get; private set; }

        public int Fatigue { get; private set; }

        #endregion


        public Miner()
        {
            Message += Miner_Receive_Message;            
        }
        #region [ Unity Monobehavior Events ]


        void Awake()
        {
            Id = Random.Range(0, 100);
            StateMachine = new StateMachine(this) { GlobalState = MinerGlobalState<Miner>.Instance };
            Messenger.AddListener<MessageEventArgs<Agent>>(MessageType.StewsReady.ToString(), OnMessage);

        }

        // Use this for initialization
        void Start()
        {
            StateMachine.ChangeState(GoHomeAndSleepTillRested<Miner>.Instance);
             
            StartCoroutine(PerformUpdate());
        }
        
        void Miner_Receive_Message(object sender, MessageEventArgs<Agent> e)
        {
            switch (e.Telegram.MessageType)
            {
                case MessageType.StewsReady:
                    Debug.Log("Message handled by " + e.Agent.Id + " at time ");
                    Say(" Okay Hun, ahm a comin'!");
                    ChangeState<Miner>(EatStew<Miner>.Instance);
                    break;
            }
        }


        IEnumerator PerformUpdate()
        {
            while (true)
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
            return string.Format("[Miner - {5}: GoldCarried={0}, MoneyInBank={1}, Thirst={2}, Fatigue={3}, LocationType={4}]\n", GoldCarried, MoneyInBank, Thirst, Fatigue, Location, Id);
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
        #endregion
    }

}
