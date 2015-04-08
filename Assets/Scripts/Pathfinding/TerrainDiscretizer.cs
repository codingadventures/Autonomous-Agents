
namespace Assets.Scripts.Pathfinding
{
    using UnityEngine;
    public class TerrainDiscretizer : MonoBehaviour
    {

        public Grid Grid;
        public float Sample;
        // Use this for initialization
        public bool DrawGrid;

        private Terrain _terrain;
        void Awake()
        {
            _terrain = GetComponent<Terrain>();

            Grid = new Grid(_terrain, Sample);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnDrawGizmos()
        {
            if (Grid == null) return;

            if (!DrawGrid) return;

            foreach (var node in Grid.InternalGrid)
            {
                if (node.IsWalkable)
                    Gizmos.color = Color.Lerp(Color.green, Color.red, 1 - node.Cost);
                else
                    Gizmos.color = Color.red;


                Gizmos.DrawSphere(node.Position, 0.2f);
            }
        }
    }
}
