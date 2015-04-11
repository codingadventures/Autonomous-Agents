using Assets.Scripts.Message;
using Assets.Scripts.Sensing;
using Assets.Scripts.States;
using UnityEngine;

namespace Assets.Scripts.Agents
{
    public class Bandit : Agent
    {

        public Bandit()
        {
            Message += BanditReceiveMessage;
        }
        protected override void Awake()
        {
            base.Awake();
            StateMachine = new StateMachine(this);
            Messenger.AddListener<MessageEventArgs<Agent>>(MessageType.BanditsFollowMe.ToString(), OnMessage);

        }

        protected override void Start()
        {
            base.Start();
            Location = LocationType.EscapePoint;
            
            StateMachine.ChangeState(RobBank<Bandit>.Instance);
            StartCoroutine(PerformUpdate());
        }

        void BanditReceiveMessage(object sender, MessageEventArgs<Agent> e)
        {
            switch (e.Telegram.MessageType)
            {
                case MessageType.BanditsFollowMe:
                    Debug.Log("Message Sent by " + e.Agent.Id + " at time ");
                    Say("OH NO! The Sheriff caught us!");
                    TargetLocation = LocationType.PunishmentHill;
                    ChangeState<Bandit>(WalkingTo<Bandit>.Instance);
                    break;
            }
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
