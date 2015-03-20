
namespace Assets.Scripts
{
    using Agents;

    public interface IStateMachine 
    {

        IState CurrentState { get; }

        IState GlobalState { get; set; }

        IState PreviousState { get; }

        Agent Agent { get; }

        void ChangeState(IState newState);

        void Update();

        void RevertToPreviousState();
    }
}
