using Assets.Scripts.Sensing;
using Assets.Scripts.States;
using UnityEngine;

namespace Assets.Scripts.Agents
{
    public class Bandit : Agent
    {
        protected override void Awake()
        {
            base.Awake();
            StateMachine = new StateMachine(this);
        }

        protected override void Start()
        {
            base.Start();

            StateMachine.ChangeState(RobBank<Bandit>.Instance);
        }

        // Use this for initialization
        public override string ToString()
        {
            var message = string.Format("<color=white>[Bandit - Id: {0}", Id);

           
            message += "]</color>";
            return message;
        }

        public override void HandleSense(Sense sense)
        {
            if (sense.AgentSensed.GetType() != typeof (Sheriff)) return;

            if (StateMachine.CurrentState == RobBank<Bandit>.Instance)
                StateMachine.ChangeState(Runaway<Bandit>.Instance);
             
        }
    }
}
