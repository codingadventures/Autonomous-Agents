

using Assets.Scripts.Message;

namespace Assets.Scripts.Agents
{
    using System;
    using UnityEngine;
    public class Agent : MonoBehaviour
    {
        public float UpdateStep;


        protected event EventHandler<MessageEventArgs<Agent>> Message;

        public IStateMachine StateMachine { get; protected set; }

        public int Id { get; set; }

        public void ChangeState<T>(IState newState)
        {
            StateMachine.ChangeState(newState);
        }

        public LocationType Location { get; protected set; }

        public void ChangeLocation(LocationType locationType)
        {
            Location = locationType;
        }

        public void Say(string vocalMessage)
        {
            var formattedMessage = string.Format("Agent ID: {0} - Message {1}", Id, vocalMessage);
            if (Debug.isDebugBuild)
                Debug.Log(formattedMessage);

        }

        protected void OnMessage(MessageEventArgs<Agent> aea)
        {
            var handler = Message;
            if (handler != null)
            {
                handler(this, aea);
            }
        }
    }
}
