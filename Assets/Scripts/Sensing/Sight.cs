using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Pathfinding;
using UnityEngine;

namespace Assets.Scripts.Sensing
{
    public class Sight : Sense
    {
        public override List<Node> PropagateSense(PathFinder pathFinder, Vector3 startPosition, Vector3 endPosition)
        {
            
            var list = pathFinder.CalculateSensingPath(startPosition, endPosition);


            return list;

        }
    }
}
