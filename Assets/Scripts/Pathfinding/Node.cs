
namespace Assets.Scripts.Pathfinding
{
    using UnityEngine;

    public class Node
    {
        public int Cost { get; private set; }

        public Vector3 Position { get; private set; }
         

        public Node(int cost, Vector3 position)
        {
            Cost = cost;
            Position = position;
        }
    }
}
