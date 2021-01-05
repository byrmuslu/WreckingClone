using Base.Game.Command;

namespace Base.Game.State
{
    public class StateMoveForwardRotateAxisY : IState
    {
        private BaseCarContext _context;

        public StateMoveForwardRotateAxisY(BaseCarContext context) 
        { 
            _context = context;
            context.Move = new Command<MovementAction>(context.MovementAction, m => m.MoveForward());
            context.Rotate = new Command<RotateAction>(context.RotateAction, r => r.RotateAxisY());
        }

        public void Handle(BaseContext context)
        {

        }
    }
}