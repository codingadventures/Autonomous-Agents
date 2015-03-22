
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
            
            var terrainSize = _terrain.terrainData.size;
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
                Gizmos.color = node.Cost == 1 ? Color.green : Color.red;
                Gizmos.DrawSphere(node.Position, 0.2f);
            }
        }
    }
}
