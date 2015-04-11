

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

        public void ChangeLocation(Vector3 location)
        {

            Path = PathFinder.CalculatePath(transform.position, location);
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
            var finders = FindObjectsOfType<PathFinder>();
            PathFinder = finders.First(p => p.name.Equals("Terrain"));
            LocationManager = FindObjectOfType<LocationManager>();
            Path = Enumerable.Empty<Vector3>();
        }

        void FixedUpdate()
        {

            if (!MoveToPoint(NodeIndex))
                return;

            NodeIndex -= 1;
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
            var adjustedPoint = point + Vector3.up * 0.5f;

            var dist = Vector3.Distance(adjustedPoint, transform.position);

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

    public enum Verbosity
    {
        Silent,
        Verbose
    }
}
