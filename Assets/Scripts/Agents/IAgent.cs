namespace Assets.Scripts.Agents
{
    public interface IAgent<T> 
    {
        IStateMachine<T> StateMachine {  get;  set; }


        int NextValidId {  get; set; }

        void ChangeState<T>(IState<T> newState);


    }
}
