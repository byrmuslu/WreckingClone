namespace Base.Game.Command
{
    public class MovementAction
    {
        private IMoveableObject _obj;

        public MovementAction(IMoveableObject obj) => _obj = obj;

        public void MoveForward() => _obj.GetRigidbody().velocity = _obj.GetRigidbody().transform.forward * _obj.GetSpeed();
        public void MoveBack() => _obj.GetRigidbody().velocity = _obj.GetRigidbody().transform.forward * -1 * _obj.GetSpeed();
        public void MoveRight() => _obj.GetRigidbody().velocity = _obj.GetRigidbody().transform.right * _obj.GetSpeed();
        public void MoveLeft() => _obj.GetRigidbody().velocity = _obj.GetRigidbody().transform.right * -1 * _obj.GetSpeed();
        public void MoveUp() => _obj.GetRigidbody().velocity = _obj.GetRigidbody().transform.up * _obj.GetSpeed();
        public void MoveDown() => _obj.GetRigidbody().velocity = _obj.GetRigidbody().transform.up * -1 * _obj.GetSpeed();

    }
}