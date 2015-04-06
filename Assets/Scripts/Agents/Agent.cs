

namespace Assets.Scripts.Agents
{
    using System;
    using UnityEngine;
    using Message;
    using System.Collections.Generic;
    using Pathfinding;

    public class Agent : MonoBehaviour
    {
        #region [ Public Field - Used By Unity Interface ]
        public float UpdateStep;

        public int MoveSpeed;
        #endregion

        #region [ Protected Field ]
        protected event EventHandler<MessageEventArgs<Agent>> Message;

        protected IEnumerable<Vector3> Path;

        protected PathFinder PathFinder;

        protected LocationManager LocationManager;
        #endregion

        #region[ Public Properties ]
        public IStateMachine StateMachine { get; protected set; }

        public int Id { get; set; }
        public LocationType Location { get; protected set; }
        #endregion

        #region [ Public Methods ]

        public void ChangeState<T>(IState newState)
        {
            StateMachine.ChangeState(newState);
        }


        public void ChangeLocation(LocationType locationType)
        {
            Location = locationType;
            var destination = LocationManager.Locations[Location];
            Path = PathFinder.CalculatePath(transform.position, destination.position);
        }

        public void Say(string vocalMessage)
        {
            var formattedMessage = string.Format("Agent ID: {0} - Message {1}", Id, vocalMessage);
            if (Debug.isDebugBuild)
                Debug.Log(formattedMessage);

        }
        #endregion

        #region [ Protected Methods ]
        protected void OnMessage(MessageEventArgs<Agent> aea)
        {
            var handler = Message;
            if (handler != null)
            {
                handler(this, aea);
            }
        }

        #endregion

        #region [ Unity Monobehavior Events ]

        protected virtual void Start()
        {
            PathFinder = FindObjectOfType<PathFinder>();
            LocationManager = FindObjectOfType<LocationManager>();
        }

        #endregion

    }
}
