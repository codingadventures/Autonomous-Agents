
namespace Assets.Scripts.Pathfinding
{
    using UnityEngine;
    public class Grid
    {
        public readonly Node[,] InternalGrid;
        private readonly Vector3 _worldPosition;

        public Grid(Vector3 worldPosition, int width, int height, float sample)
        {
            var w = (int)Mathf.Ceil(width / sample);
            var h = (int)Mathf.Ceil(height / sample);

            InternalGrid = new Node[w, h];
            _worldPosition = worldPosition;

            InitGrid(w, h, sample);
        }

        private void InitGrid(int w, int h, float sample)
        {
            for (var i = 0; i < w; i++)
            {
                for (var j = 0; j < h; j++)
                {
                    var x = sample * 0.5f * (2 * i + 1); //I calculate the center of each grid i * sample + sample / 2
                    var z = sample * 0.5f * (2 * j + 1);

                    var pos = new Vector3(x, 0, z) + _worldPosition;

                    var terrainheight = Terrain.activeTerrain.SampleHeight(pos);
                    pos.y = terrainheight;
                    var dir = new Vector3(0, -10, 0);
                    RaycastHit hit;
                    var intersect = Physics.Raycast(pos + new Vector3(0, 10, 0), dir, out hit, 10);
                    var cost = intersect ? int.MaxValue : 1;

                    InternalGrid[i, j] = new Node(cost, pos);
                }
            }
        }

        //Node[] FindPath(Vector3 start, Vector3 end)
        //{
            



        //}
    }
}
