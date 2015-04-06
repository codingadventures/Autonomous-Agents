using System;
using Assets.Scripts.Message;

namespace Assets.Scripts.Agents
{
    using States;
    using UnityEngine;
    using System.Collections;

    public class Elsa : Agent
    { 
         
        public bool Cooking
        {
            get;
            set;
        }

        public Elsa()
        {
            Message += ElsaReceiveMessage;            
        }

        void ElsaReceiveMessage(object sender, MessageEventArgs<Agent> e)
        {
            switch (e.Telegram.MessageType)
            {
                case MessageType.HiHoneyImHome:
                    Debug.Log("Message Sent by " + e.Agent.Id + " at time ");
                    Say("Hi honey. Let me make you some of mah fine country stew");
                    ChangeState<Elsa>(CookStew<Elsa>.Instance);
                    break;
            }
        }

        #region [ Overrides ]
        public override string ToString()
        {
            var message = string.Format("[<color=green>Wife Elsa - Id: {0}", Id);

            if (Verbosity == Verbosity.Verbose)
                message += string.Format("Cooking={0}, LocationType={1}]" + Environment.NewLine, Cooking, Location);

            message += "</color>";
            return message;
        }
        #endregion


        #region [ Unity Monobehavior Events ]


        void Awake()
        {
            Id = Random.Range(0, 100);
            StateMachine = new StateMachine(this) {GlobalState = WifeGlobalState<Elsa>.Instance};
            Messenger.AddListener<MessageEventArgs<Agent>>(MessageType.HiHoneyImHome.ToString(), OnMessage);

        }

     
        protected override void Start()
        {
            base.Start();

            StateMachine.ChangeState(DoHousework<Elsa>.Instance);
            StartCoroutine(PerformUpdate());
        }


        IEnumerator PerformUpdate()
        {
            while (true)
            {
                StateMachine.Update();

                yield return new WaitForSeconds(UpdateStep);
            }
        }

        #endregion 
    }
}
