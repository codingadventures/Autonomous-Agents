using Assets.Scripts.Sensing;
using Assets.Scripts.States;
using UnityEngine;

namespace Assets.Scripts.Agents
{
    public class Sheriff : Agent
    {
        protected override void Awake()
        {
            base.Awake();
            StateMachine = new StateMachine(this);
        }

        protected override void Start()
        {
            base.Start();

            StateMachine.ChangeState(PatrolTown<Sheriff>.Instance);

            StartCoroutine(PerformUpdate());
        }

        // Use this for initialization
        public override string ToString()
        {
            var message = string.Format("<color=yellow>[Sheriff- Id: {0}", Id);


            message += "]</color>";
            return message;
        }


        private static bool IsAgentBandit(Agent agent)
        {
            return agent.GetType() == typeof(Bandit);
        }
        public override void HandleSense(Sense sense)
        {

            var agent = sense.AgentSensed;

            if (IsAgentBandit(agent))
            {
                //Let's chase them
                if (StateMachine.CurrentState != ChaseBandits<Sheriff>.Instance &&
                    StateMachine.CurrentState != PunishBandits<Sheriff>.Instance)
                    StateMachine.ChangeState(ChaseBandits<Sheriff>.Instance);
            }
            else
            {
                Say(sense.AgentSensed.GetType() == typeof(Miner)
                    ? string.Format("Mornin' Bob!:{0}", sense.AgentSensed.Id)
                    : string.Format("Good morning Mylady!:{0}", sense.AgentSensed.Id));
            }



        }
    }
}
