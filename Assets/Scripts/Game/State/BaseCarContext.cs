namespace Base.Game.State
{
    using Base.Game.Command;
    using Base.Game.GameObject.Interactional;

    public class BaseCarContext : BaseContext, IBaseCarContext
    {
        public MovementAction MovementAction { get; private set; }
        public ICommand Move { get; set; }
        public RotateAction RotateAction { get; private set; }
        public ICommand Rotate { get; set; }
        public BaseCarContext(BaseCar car) 
        {
            MovementAction = new MovementAction(car);
            RotateAction = new RotateAction(car);
        }

    }
}