namespace Base.Game.Command
{
    public class MovementAction
    {
        private IMoveableObject _obj;

        public MovementAction(IMoveableObject obj) => _obj = obj;

        public void MoveForward() => _obj.Rigidbody.velocity = _obj.Rigidbody.transform.forward * _obj.Speed;
        public void MoveBack() => _obj.Rigidbody.velocity = _obj.Rigidbody.transform.forward * -1 * _obj.Speed;
        public void MoveRight() => _obj.Rigidbody.velocity = _obj.Rigidbody.transform.right * _obj.Speed;
        public void MoveLeft() => _obj.Rigidbody.velocity = _obj.Rigidbody.transform.right * -1 * _obj.Speed;
        public void MoveUp() => _obj.Rigidbody.velocity = _obj.Rigidbody.transform.up * _obj.Speed;
        public void MoveDown() => _obj.Rigidbody.velocity = _obj.Rigidbody.transform.up * -1 * _obj.Speed;

    }
}