namespace Assets.Scripts
{
    public interface IStateMachine<T>  
    {

        IState<T> CurrentState { get;}

        IState<T> GlobalState { get;  set; }

        IState<T> PreviousState { get;}

        T Agent { get; }// { get; protected set; }

        void ChangeState(IState<T> newState);
        
        void Update();
    }
}
