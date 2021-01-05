namespace Base.Game.State
{
    using Base.Game.Command;
    public interface IBaseCarContext
    {
        public ICommand Move { get; set; }
        public ICommand Rotate { get; set; }
        public IState State { get; set; }
    }
}