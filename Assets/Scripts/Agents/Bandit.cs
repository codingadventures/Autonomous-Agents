using Assets.Scripts.Sensing;
using UnityEngine;

namespace Assets.Scripts.Agents
{
    public class Bandit : Agent
    {

        // Use this for initialization
        public override string ToString()
        {
            var message = string.Format("<color=white>[Bandit - Id: {0}", Id);

           
            message += "]</color>";
            return message;
        }

        public override void HandleSense(Sense sense)
        {
            throw new System.NotImplementedException();
        }
    }
}
