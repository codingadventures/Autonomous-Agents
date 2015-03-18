namespace Assets.Scripts
{
    public interface IAgent<T> 
    {
        IStateMachine<T> StateMachine {  get;  set; }

        int NextValidId {  get; set; }
    }
}
