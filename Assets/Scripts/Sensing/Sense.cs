using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Agents;
using UnityEngine;

namespace Assets.Scripts.Sensing
{
    public abstract class Sense
    {

        public Agent AgentSensed { get; set; }

        public abstract bool PropagateSense(Vector3 startPosition, Vector3 endPosition);
    }
}
