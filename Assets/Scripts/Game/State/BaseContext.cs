namespace Base.Game.State
{
    public class BaseContext
    {
        public virtual IState State { get; set; }

        public void Request() => State?.Handle(this);
    }
}