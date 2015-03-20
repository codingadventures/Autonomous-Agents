
namespace Assets.Scripts.Pathfinding
{
    using UnityEngine;
    public class Plane : MonoBehaviour
    {

        private Grid _grid;

        // Use this for initialization
        void Start()
        {
            var terrainSize = Terrain.activeTerrain.terrainData.size;
            _grid = new Grid(Terrain.activeTerrain.GetPosition(), (int)terrainSize.x, (int)terrainSize.z, 0.5f);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnDrawGizmos()
        {
            if (_grid == null) return;
            
            foreach (var node in _grid.InternalGrid)
            {
                Gizmos.color = node.Cost == 1 ? Color.green : Color.red;
                Gizmos.DrawSphere(node.Position, 0.2f);
            }
        }
    }
}
