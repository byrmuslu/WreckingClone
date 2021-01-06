namespace Base.Game.Command
{
    public class MovementAction
    {
        private IMoveableObject _obj;

        public MovementAction(IMoveableObject obj) => _obj = obj;

        public void MoveForward() 
        {
            var direc = _obj.Transform.forward * _obj.Speed;
            direc.y = _obj.Rigidbody.velocity.y;
            _obj.Rigidbody.velocity = direc;
        }
        public void MoveBack()
        {
            var direc = _obj.Transform.forward * _obj.Speed * -1;
            direc.y = _obj.Rigidbody.velocity.y;
            _obj.Rigidbody.velocity = direc;
        }
        public void MoveRight()
        {
            var direc = _obj.Transform.right * _obj.Speed;
            direc.y = _obj.Rigidbody.velocity.y;
            _obj.Rigidbody.velocity = direc;
        }
        public void MoveLeft()
        {
            var direc = _obj.Transform.right * _obj.Speed * -1;
            direc.y = _obj.Rigidbody.velocity.y;
            _obj.Rigidbody.velocity = direc;
        }
        public void MoveUp()
        {
            var direc = _obj.Transform.up * _obj.Speed;
            direc.y = _obj.Rigidbody.velocity.y;
            _obj.Rigidbody.velocity = direc;
        }
        public void MoveDown()
        {
            var direc = _obj.Transform.up * -1 * _obj.Speed;
            direc.y = _obj.Rigidbody.velocity.y;
            _obj.Rigidbody.velocity = direc;
        }

    }
}