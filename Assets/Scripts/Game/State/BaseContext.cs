namespace Base.Game.State
{
    public class BaseContext
    {
        public IState State { get; set; }

        public void Request() => State?.Handle(this);
    }
}