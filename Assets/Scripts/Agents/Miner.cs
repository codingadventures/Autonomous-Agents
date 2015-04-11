

namespace Assets.Scripts.Agents
{
    using States;
    using UnityEngine;
    using Message;

    using System;
    using System.Linq;
    using Sensing;

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

        #region [ Constructor ]
        public Miner()
        {
            Message += Miner_Receive_Message;
        }
        #endregion

        #region [ Unity Monobehavior Events ]



        protected override void Awake()
        {
            StateMachine = new StateMachine(this) { GlobalState = MinerGlobalState<Miner>.Instance };
            Messenger.AddListener<MessageEventArgs<Agent>>(MessageType.StewsReady.ToString(), OnMessage);
        }

        // Use this for initialization
        protected override void Start()
        {
            base.Start();

            StateMachine.ChangeState(SleepTillRested<Miner>.Instance);

            StartCoroutine(PerformUpdate());
        }

        void FixedUpdate()
        {

            if (!MoveToPoint(NodeIndex))
                return;

            NodeIndex -= 1;
        }



        #endregion

        #region [ Overrides ]

        public override string ToString()
        {
            var message = string.Format("<color=red>[Miner Bob - Id: {0}", Id);

            if (Verbosity == Verbosity.Verbose)
                message += String.Format("GoldCarried={0}, MoneyInBank={1}, Thirst={2}, Fatigue={3}, LocationType={4}]" + Environment.NewLine, GoldCarried, MoneyInBank, Thirst, Fatigue, Location);

            message += "]</color>";
            return message;
        }

        public override void HandleSense(Sense sense)
        {
            throw new NotImplementedException();
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

        public bool IsRested()
        {
            return Fatigue == 0;
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


        #region [ Private Methods ]
        void Miner_Receive_Message(object sender, MessageEventArgs<Agent> e)
        {
            switch (e.Telegram.MessageType)
            {
                case MessageType.StewsReady:
                    if (Location != LocationType.HomeSweetHome) return;

                    Debug.Log("Message handled by " + e.Agent.Id + " at time ");
                    Say(" Okay Hun, ahm a comin'!");
                    ChangeState<Miner>(EatStew<Miner>.Instance);
                    break;
            }
        }

        private bool MoveToPoint(int nodeIndex)
        {
            if (nodeIndex < 0) return false;

            if (!Path.Any()) return false;

            if (Path.Count() <= nodeIndex) return false;

            var point = Path.ElementAt(nodeIndex);
             
            //this is for dynamic waypoint, each unit creep have it's own offset pos
            //point+=dynamicOffset;
            // point += pathDynamicOffset;//+flightHeightOffset;
            var adjustedPoint = point + Vector3.up*0.5f;

            float dist = Vector3.Distance(adjustedPoint, transform.position);

            //if the unit have reached the point specified
            //~ if(dist<0.15f) return true;
            if (dist < 0.005f) return true;

            //rotate towards destination
            //if (moveSpeed > 0)
            //{
            //    Quaternion wantedRot = Quaternion.LookRotation(point - transform.position);
            //    //thisT.rotation = Quaternion.Slerp(thisT.rotation, wantedRot, rotateSpd * Time.deltaTime);
            //}

            //move, with speed take distance into accrount so the unit wont over shoot
            Vector3 dir = (adjustedPoint - transform.position).normalized;
            transform.Translate(dir * Mathf.Min(dist, MoveSpeed * Time.fixedDeltaTime), Space.World);
             //distFromDestination -= (MoveSpeed * Time.fixedDeltaTime);

            return false;
        }


        #endregion

    }

}
