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
        private readonly List<Agent> _agents = new List<Agent>();
        private Pathfinding.PathFinder _pathFinder;
        private List<List<Vector3>> _nodeGraph;

        // Use this for initialization
        void Start()
        {
            _nodeGraph = new List<List<Vector3>>();
            _agents.AddRange(FindObjectsOfType<Agent>());
            _pathFinder = GetComponent<PathFinder>();
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

                        if (Vector3.Distance(_agents[i].transform.position, _agents[j].transform.position) <= SenseDistance)
                        {
                            //See if the sense propagation would work
                            _nodeGraph.Add(_pathFinder.CalculatePath(_agents[i].transform.position, _agents[j].transform.position).ToList());
                        }
                    }
                }

                yield return new WaitForSeconds(1.0f);

            }
        } 

        public void OnDrawGizmos()
        {
            if (_nodeGraph == null) return;

            foreach (var node in _nodeGraph.SelectMany(nodes => nodes))
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawSphere(node, .15f);
            }
        }
    }
}
