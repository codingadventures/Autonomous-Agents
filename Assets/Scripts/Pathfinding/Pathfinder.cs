
using System;

namespace Assets.Scripts.Pathfinding
{
    using UnityEngine;
    using System.Collections.Generic;

    public enum SearchType
    {
        AStar,
        BreadthFirstSearch,
        Both
    }

    public class Pathfinder : MonoBehaviour
    {

        public GameObject Target;

        private List<Node> _breadthFirstSearchPath;
        private List<Node> _aStarPath;

        private Vector3 _position;
        private TerrainDiscretizer _terrainDiscretizer;

        public SearchType SearchType;

        // Use this for initialization
        void Start()
        {
            _terrainDiscretizer = FindObjectOfType<TerrainDiscretizer>();

            CalculatePath();
           
        }

        // Update is called once per frame
        void Update()
        {
            if (_position == transform.position) return;

            CalculatePath();
            
        }

        public void OnDrawGizmos()
        {
            if (_breadthFirstSearchPath != null)

                foreach (var node in _breadthFirstSearchPath)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(node.Position, 0.2f);
                }

            if (_aStarPath != null)
                foreach (var node in _aStarPath)
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawSphere(node.Position, 0.2f);
                }
        }

        private void CalculatePath()
        {
            switch (SearchType)
            {
                case SearchType.AStar:
                    _aStarPath = _terrainDiscretizer.Grid.AStarSearch(transform.position, Target.transform.position);

                    break;
                case SearchType.BreadthFirstSearch:
                    _breadthFirstSearchPath = _terrainDiscretizer.Grid.BreadthFirstSearch(transform.position, Target.transform.position);

                    break;
                case SearchType.Both:
                    _aStarPath = _terrainDiscretizer.Grid.AStarSearch(transform.position, Target.transform.position);
                    _breadthFirstSearchPath = _terrainDiscretizer.Grid.BreadthFirstSearch(transform.position, Target.transform.position);

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _position = transform.position;

        }

    }
}
