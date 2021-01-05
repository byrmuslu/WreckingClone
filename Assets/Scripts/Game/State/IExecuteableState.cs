namespace Base.Game.State
{
    public interface IExecuteableState : IState
    {
        void Execute();
    }
}