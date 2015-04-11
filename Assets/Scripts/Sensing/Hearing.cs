using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Pathfinding;
using UnityEngine;

namespace Assets.Scripts.Sensing
{
    public class Hearing : Sense
    {
        public override List<Node> PropagateSense(PathFinder pathFinder, Vector3 startPosition, Vector3 endPosition)
        {
            throw new NotImplementedException();
        }
    }
}
