

using System.Collections;
using System.Linq;
using Assets.Scripts.Sensing;

namespace Assets.Scripts.Agents
{
    using System;
    using UnityEngine;
    using Message;
    using System.Collections.Generic;
    using Pathfinding;

    public abstract class Agent : MonoBehaviour, ISense
    {
        #region [ Public Field - Used By Unity Interface ]
        public float UpdateStep;

        public int MoveSpeed;

        public Verbosity Verbosity;
        #endregion

        #region [ Protected Field ]
        protected event EventHandler<MessageEventArgs<Agent>> Message;

        protected IEnumerable<Vector3> Path;

        protected PathFinder PathFinder;

        protected int NodeIndex;
        #endregion

        #region[ Public Properties ]
        public IStateMachine StateMachine { get; protected set; }

        public LocationManager LocationManager { get; protected set; }

        public int Id { get; set; }
        public LocationType Location { get; protected set; }
        public LocationType TargetLocation { get; set; }
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
            NodeIndex = Path.Count() - 1;
        }

        public void Say(string vocalMessage)
        {
            var formattedMessage = string.Format("{0} Message {1}", ToString(), vocalMessage);

            if (Debug.isDebugBuild)
                Debug.Log(formattedMessage);

        }
        #endregion


        #region [ Abstract ]
        public abstract override string ToString();
        public abstract void HandleSense(Sense sense);

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

    

        protected IEnumerator PerformUpdate()
        {
            while (true)
            {
                StateMachine.Update();

                yield return new WaitForSeconds(UpdateStep);
            }
        }

        #endregion

        #region [ Unity Monobehavior Events ]

        protected virtual void Awake()
        {
            Id = UnityEngine.Random.Range(0, 100);
        }
        protected virtual void Start()
        {
            PathFinder = FindObjectOfType<PathFinder>();
            LocationManager = FindObjectOfType<LocationManager>();
            Path = Enumerable.Empty<Vector3>();
        }

        #endregion

    }

    public enum Verbosity
    {
        Silent,
        Verbose
    }
}
