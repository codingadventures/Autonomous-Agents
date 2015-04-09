
namespace Assets.Scripts.Pathfinding
{
    using UnityEngine;
    public class TerrainDiscretizer : MonoBehaviour
    {

        public Grid Grid;
        public float Sample;
        // Use this for initialization
        public bool DrawGrid;
        
        public float HeightCost;

        private Terrain _terrain;
        void Awake()
        {
            _terrain = GetComponent<Terrain>();

            Grid = new Grid(_terrain, Sample, HeightCost);
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
                Gizmos.color = 
                    node.IsWalkable 
                    ? Color.Lerp(Color.green, Color.red, node.Cost / (10 * 2.5f)) 
                    : Color.red;


                Gizmos.DrawSphere(node.Position, 0.2f);
            }
        }
    }
}
