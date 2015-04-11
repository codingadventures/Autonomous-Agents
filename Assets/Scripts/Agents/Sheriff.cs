using Assets.Scripts.Sensing;
using UnityEngine;

namespace Assets.Scripts.Agents
{
    public class Sheriff : Agent {

        // Use this for initialization
        public override string ToString()
        {
            var message = string.Format("<color=yellow>[Sheriff- Id: {0}", Id);

             
            message += "]</color>";
            return message;
        }


        private bool IsAgentBandit(Agent agent)
        {
            return agent.GetType() == typeof (Bandit);
        }
        public override void HandleSense(Sense sense)
        {
            if (!sense.PropagateSense(transform.position, sense.AgentSensed.transform.position)) return;

            var agent = sense.AgentSensed;

            if (IsAgentBandit(agent))
            {
                //Let's chase them
            }
            else
            {
                //politely say hello
            }



        }
    }
}
