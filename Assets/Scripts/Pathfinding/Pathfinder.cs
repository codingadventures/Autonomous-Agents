 namespace Assets.Scripts.Pathfinding
{
    using UnityEngine;
    using System.Collections.Generic;

    using System;
    using System.Linq;

    public enum SearchType
    {
        AStar,
        BreadthFirstSearch,
        Both
    }

    public class PathFinder : MonoBehaviour
    {

        private List<Node> _nodeGraph;
        private TerrainDiscretizer _terrainDiscretizer;
        public SearchType SearchType;

        // Use this for initialization
        void Start()
        {
            _terrainDiscretizer = FindObjectOfType<TerrainDiscretizer>();

        }



        public void OnDrawGizmos()
        {
            if (_nodeGraph == null) return;

            foreach (var node in _nodeGraph)
            {
                if (!node.IsWalkable)
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawSphere(node.Position, _terrainDiscretizer.Sample * .15f);
                }
                else
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawSphere(node.Position, _terrainDiscretizer.Sample * .15f);
                }
            }
        }

        public IEnumerable<Vector3> CalculatePath(Vector3 start, Vector3 end  )
        {
            switch (SearchType)
            {
                case SearchType.AStar:
                    _nodeGraph = _terrainDiscretizer.Grid.AStarSearch(start, end);

                    break;
                case SearchType.BreadthFirstSearch:
                    _nodeGraph = _terrainDiscretizer.Grid.BreadthFirstSearch(start, end);

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return _nodeGraph.Select(node => node.Position) ;


        }

        public Node GetNearestNode(Vector3 startPosition)
        {
            return _terrainDiscretizer.Grid.FindNode(startPosition);
        }

        public List<Vector3> SmoothPath(List<Vector3> altPath)
        {
            throw new NotImplementedException();
        }

        public List<Vector3> ForceSearch(Node startN, Node endN, Node targetNode)
        {
            throw new NotImplementedException();
        }

    }
}
