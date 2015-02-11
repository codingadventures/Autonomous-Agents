using UnityEngine;
using System.Collections;
using Assets.Scripts.States;
namespace Assets.Scripts
{

    public class Miner : IAgent<Miner>
    {
        public int GoldCarried;

        public int MoneyInBank;

        public int Thirst;

        public int Fatigue;

        public LocationType LocationType;

        public IStateMachine<Miner> StateMachine
        {
            get;
            set;
        }

        public int NextValidId { get; set; }

        void Awake()
        {
            NextValidId = Random.Range(0, 100);
            StateMachine = new StateMachine<Miner>(this);
        }

        // Use this for initialization
        void Start()
        {

        }


        // Update is called once per frame
        void Update()
        {
            Thirst += 1;

            StateMachine.Update();            
        }
    }

}
