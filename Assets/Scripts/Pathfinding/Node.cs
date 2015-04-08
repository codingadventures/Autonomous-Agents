
using System.Collections.Generic;

namespace Assets.Scripts.Pathfinding
{
    using UnityEngine;

    public class Node
    {
        private readonly Location _gridPosition;

        public float Cost { get; set; }

        public Vector3 Position { get; private set; }

        public List<Node> Neighboors { get; private set; }

        public readonly bool IsWalkable; //{ get; private set; }

        public Node(float cost, Location gridPosition, Vector3 position, bool isWalkable)
        {
            _gridPosition = gridPosition;
            Cost = cost;
            Position = position;
            Neighboors = new List<Node>(4);
            IsWalkable = isWalkable;
        }



        public override string ToString()
        { 
            return _gridPosition.ToString();
        }


    }

    internal class NodeComparer : IComparer<Node>
    {
        public int Compare(Node x, Node y)
        {
            if (x.Cost > y.Cost) return 1;
            if (x.Cost < y.Cost) return -1;

            return 0;
        }
    }

    internal class NodeEqualityComparer : IEqualityComparer<Node>
    {
        public bool Equals(Node x, Node y)
        {
            return x.ToString().Equals(y.ToString());
        }

        public int GetHashCode(Node obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
