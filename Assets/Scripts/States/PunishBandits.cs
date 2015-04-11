using System;
using Assets.Scripts.Agents;

namespace Assets.Scripts.States
{
    public class PunishBandits<T> : State where T : Sheriff
    {
        #region [ Singleton Implementation ]
        public static PunishBandits<T> Instance { get { return Nested.instance; } }

        private PunishBandits()
        {
            Enter += PunishBandits_Enter;
        }



        /// This is a fully lazy initialization implementation
        /// Instantiation is triggered by the first reference to the static member of the nested class, 
        /// which only occurs in Instance. This means the implementation is fully lazy.
        /// Note that although nested classes have access to the enclosing class's private members, the reverse is not true, 
        /// hence the need for instance to be internal here. That doesn't raise any other problems, though, as the class itself is private. 
        /// The code is a bit more complicated in order to make the instantiation lazy, however.
        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly PunishBandits<T> instance = new PunishBandits<T>();
        }
        #endregion

       

        private void PunishBandits_Enter(object sender, AgentEventArgs<Agent> e)
        {
            e.Agent.TargetLocation = LocationType.PunishmentHill;
           
            if (e.Agent.Location != e.Agent.TargetLocation)
                e.Agent.ChangeState<T>(WalkingTo<T>.Instance);

            e.Agent.Say(string.Format("Bandits you'll be punished! follow me!"));
        }
        public override void Execute(Agent agent)
        {
              agent.Say("This is the punishment you DESERVE! BANG BANG!");
        }
    }
}
