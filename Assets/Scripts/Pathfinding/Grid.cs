namespace Assets.Scripts.Pathfinding
{
    using UnityEngine;
    using C5;
    using System.Collections.Generic;
    using System.Linq;

    public class Grid
    {
        public readonly Node[,] InternalGrid;
        private readonly Vector3 _worldPosition;
        public int SampledWidth { get; private set; }
        public int SampledHeight { get; private set; }

        private readonly float _sample;
        private readonly float _heightCost;
        private readonly Terrain _terrain;
        private readonly List<Location> _directions = new List<Location>
        {
            new Location(1, 0),
            new Location(0, 1),
            new Location(-1, 0),
            new Location(0, -1),
            new Location(1, 1),
            new Location(-1, -1),
            new Location(-1,  1),
            new Location( 1,  -1),

        };

        public Grid(Terrain terrain, float sample, float heightCost)
        {
            _terrain = terrain;

            _worldPosition = terrain.GetPosition();
            var width = (int)terrain.terrainData.size.x;
            var height = (int)terrain.terrainData.size.z;

            SampledWidth = (int)Mathf.Ceil(width / sample);
            SampledHeight = (int)Mathf.Ceil(height / sample);
            _sample = sample;
            _heightCost = heightCost;
            InternalGrid = new Node[SampledWidth, SampledHeight];

            //Build the Grid [width,height] --> O(n^2)
            InitGrid();
            //Initialize the edges/neighboots --> O(n^2) 
            InitEdges();
        }

        private void InitGrid()
        {
            LayerMask mask = 1 << 2; //it's ignore raycast

            for (var i = 0; i < SampledWidth; i++)
            {
                for (var j = 0; j < SampledHeight; j++)
                {
                    var x = _sample * 0.5f * (2 * i + 1); //I calculate the center of each grid i * sample + sample / 2
                    var z = _sample * 0.5f * (2 * j + 1);
                    float attenuation = 0;
                    var pos = new Vector3(x, 0, z) + _worldPosition;

                    var terrainheight = _terrain.SampleHeight(pos);
                    pos.y = terrainheight;

                    RaycastHit hit;
                    var intersect = Physics.Raycast(pos + new Vector3(0, 10, 0), Vector3.down, out hit, Mathf.Infinity, ~mask);
                    if (intersect)
                    {
                        var colliderSize = hit.collider.bounds.size;
                        var colliderHeight = colliderSize.y;
                        attenuation = colliderHeight;
                    }

                    var cost = intersect ? int.MaxValue : 1 + 1 * terrainheight * _heightCost;
                    attenuation = 0.1f * (terrainheight + attenuation);
                    var node = new Node(cost, attenuation, new Location(i, j), pos, isWalkable: !intersect);
                    InternalGrid[i, j] = node;
                }
            }
        }

        private void InitEdges()
        {
            for (var i = 0; i < SampledWidth; i++)
            {
                for (var j = 0; j < SampledHeight; j++)
                {
                    var node = InternalGrid[i, j];

                    var neighboors = FindNeighboors(node, i, j);
                    node.Neighboors.AddRange(neighboors);
                }
            }
        }

        private IEnumerable<Node> FindNeighboors(Node node, int i, int j)
        {
            var neighboors = new List<Node>(4);

            foreach (var point in _directions)
            {
                if (0 > i + point.X || i + point.X >= SampledWidth || 0 > j + point.Y || j + point.Y >= SampledHeight)
                    continue;

                var neighNode = InternalGrid[i + point.X, j + point.Y];

                if (neighNode.IsWalkable)
                    neighboors.Add(neighNode);
            }

            return neighboors;
        }

        public List<Node> BreadthFirstSearch(Vector3 start, Vector3 end)
        {
            var frontier = new Queue<Node>();
            var cameFrom = new Dictionary<string, Node>();
            var startNode = FindNode(start);
            var endNode = FindNode(end);

            frontier.Enqueue(startNode);
            cameFrom.Add(startNode.ToString(), null);

            while (frontier.Any())
            {
                //var a = frontier.FindMin();
                //frontier.DeleteMin();
                var node = frontier.Dequeue();
                if (node == endNode)
                    break;
                foreach (var neighboor in node.Neighboors.Where(neighboor => !cameFrom.ContainsKey(neighboor.ToString())))
                {
                    frontier.Enqueue(neighboor);
                    cameFrom.Add(neighboor.ToString(), node);
                }

            }

            var current = endNode;
            var path = new List<Node> { current };

            while (current != startNode)
            {
                if (!cameFrom.ContainsKey(current.ToString())) continue;

                current = cameFrom[current.ToString()];
                path.Add(current);
            }

            return path;
        }

        public List<Node> AStarSearch(Vector3 start, Vector3 end)
        {
            var frontier = new IntervalHeap<Node>(new NodeComparer());
            var cameFrom = new Dictionary<string, Node>();
            //var costSoFar = new Dictionary<string, int>();

            var startNode = FindNode(start);
            var endNode = FindNode(end);

            frontier.Add(startNode);
            cameFrom.Add(startNode.ToString(), null);
            //costSoFar.Add(startNode.ToString(), 0);

            while (frontier.Any())
            {
                var currentNode = frontier.FindMin();
                frontier.DeleteMin();

                if (currentNode == endNode)
                    break;

                foreach (var neighbor in currentNode.Neighboors)
                {
                    var neighborName = neighbor.ToString();

                    if (cameFrom.ContainsKey(neighborName))
                        continue;

                    var newCost = Heuristic(endNode, neighbor);

                    neighbor.Cost = newCost;

                    frontier.Add(neighbor);
                    cameFrom.Add(neighborName, currentNode);
                }
            }

            var current = endNode;
            var path = new List<Node> { current };

            if (!cameFrom.ContainsKey(endNode.ToString())) return path;

            while (current != startNode)
            {
                if (!cameFrom.ContainsKey(current.ToString())) continue;

                current = cameFrom[current.ToString()];
                path.Add(current);
            }

            return path;
        }


        public List<Node> AStarSensing(Vector3 start, Vector3 end)
        {
            var frontier = new IntervalHeap<Node>(new NodeAttenuationComparer());
            var cameFrom = new Dictionary<string, Node>();

            var startNode = FindNode(start);
            var endNode = FindNode(end);

            frontier.Add(startNode);
            cameFrom.Add(startNode.ToString(), null);

            while (frontier.Any())
            {
                var currentNode = frontier.FindMin();
                frontier.DeleteMin();

                if (currentNode == endNode)
                    break;

                foreach (var neighbor in currentNode.Neighboors)
                {
                    var neighborName = neighbor.ToString();

                    if (cameFrom.ContainsKey(neighborName))
                        continue;

                    var newCost = neighbor.Attenuation + Heuristic(endNode, neighbor);

                    neighbor.Cost = newCost;

                    frontier.Add(neighbor);
                    cameFrom.Add(neighborName, currentNode);
                }
            }

            var current = endNode;

            var path = new List<Node> { current };

            if (!cameFrom.ContainsKey(endNode.ToString())) return path; 

           
            while (current != startNode)
            {
                if (!cameFrom.ContainsKey(current.ToString())) continue;

                current = cameFrom[current.ToString()];
                path.Add(current);
            }

            return path;
        }


        public Node FindNode(Vector3 positionVector3)
        {
            var i = Mathf.RoundToInt(positionVector3.x / _sample);
            var j = Mathf.RoundToInt(positionVector3.z / _sample);

            i = Mathf.Clamp(i, 0, SampledWidth);
            j = Mathf.Clamp(j, 0, SampledHeight);

            return InternalGrid[i, j];

        }

        public Vector3 FindNodePosition(Vector3 position)
        {
            var node = FindNode(position);

            return node.Position;
        }


        /// <summary>
        /// Heuristics on the specified Node based on the Manhattan distance
        /// </summary>
        /// <param name="a">The Node a.</param>
        /// <param name="b">The goal Node b.</param>
        /// <returns></returns>
        static float Heuristic(Node a, Node b)
        {
            var dx = Mathf.Abs(a.Position.x - b.Position.x);
            var dz = Mathf.Abs(a.Position.z - b.Position.z);
            var dy = Mathf.Abs(a.Position.y - b.Position.y);

            return 1.0f * (dx + dy + dz);
        }

        public bool IsPositionWalkable(Vector3 position)
        {
            var node = FindNode(position);
            return node.IsWalkable;
        }
    }
}
