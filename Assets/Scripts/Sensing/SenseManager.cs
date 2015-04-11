using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Agents;
using Assets.Scripts.Pathfinding;
using UnityEngine;

namespace Assets.Scripts.Sensing
{
    public class SenseManager : MonoBehaviour
    {
        public float SenseDistance;
        public PathFinder PathFinder;

        public float MaxAttenuation;

        private readonly List<Agent> _agents = new List<Agent>();
        private List<Node> _nodeGraph;

        // Use this for initialization
        void Start()
        {
            _nodeGraph = new List<Node>();
            _agents.AddRange(FindObjectsOfType<Agent>());
            PathFinder = GetComponent<PathFinder>();
            StartCoroutine(PerformUpdate());
        }


        IEnumerator PerformUpdate()
        {
            while (true)
            {
                for (var i = 0; i < _agents.Count; i++)
                {
                    for (var j = 0; j < _agents.Count; j++)
                    {
                        if (i == j) continue;

                        if (Vector3.Distance(_agents[i].transform.position, _agents[j].transform.position) > SenseDistance) continue;


                        var sense = new Sight { AgentSensed = _agents[j] };

                        _nodeGraph = sense.PropagateSense(PathFinder, _agents[i].transform.position, _agents[j].transform.position);

                        var attenuation = _nodeGraph.AsEnumerable().Sum(node => node.Attenuation);

                        if (attenuation > MaxAttenuation) continue;

                        _agents[i].HandleSense(sense);

                    }
                }

                yield return new WaitForSeconds(.5f);

            }
        }

        public void OnDrawGizmos()
        {
            if (_nodeGraph == null) return;

            foreach (var node in _nodeGraph )
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawSphere(node.Position, .15f);
            }
        }
    }
}
