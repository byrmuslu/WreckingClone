namespace Base.Game.GameObject.Interactional
{
    using Base.Game.Command;
    using Base.Game.GameObject.Interactable;
    using Base.Game.Signal;
    using UnityEngine;

    public partial class BaseCar : IInteractionalObject, IMoveableObject, IRotateableObject
    {
        public float RotateSpeed { get; protected set; }
        public Vector3 CenterPoint { get => transform.position; }
        public Transform Transform { get => transform; }

        public Rigidbody Rigidbody { get; protected set; }
        public float Speed { get; protected set; }

        private ICommand _move;
        private ICommand _rotateRight;
        private ICommand _rotateLeft;

        protected virtual void Initialize()
        {
            var moveAction = new MovementAction(this);
            _move = new Command<MovementAction>(moveAction, m => m.MoveForward());
            var rotateAction = new RotateAction(this);
            _rotateRight = new Command<RotateAction>(rotateAction, r => r.RotateAxisY());
            _rotateLeft = new Command<RotateAction>(rotateAction, r => r.RotateAxisYMinus());
        }

        protected virtual void Movement()
        {
            _move.Execute();
        }

        protected virtual void RotateRight()
        {
            Rigidbody.angularVelocity = Vector3.zero;
            _rotateRight.Execute();
            _connectedBall.AddForce(true);
        }

        protected virtual void RotateLeft()
        {
            Rigidbody.angularVelocity = Vector3.zero;
            _rotateLeft.Execute();
            _connectedBall.AddForce(false);
        }

        public virtual void Interact(IInteractableObject obj)
        {
            if(obj is IBall ball)
            {
                Rigidbody.AddExplosionForce(ball.ImpactForce, ball.Transform.position, 30f,3.0f);
                Handheld.Vibrate();
            }
            if (obj is MagicBox box)
                _connectedBall.ChangeState(box.Timer);
        }

        public virtual void Active()
        {
            _connectedBall.Active();
            _connectedBall.Init();
            
            gameObject.SetActive(true);
        }

        public virtual void DeActive()
        {
            gameObject.SetActive(false);
            SignalBus<SignalInteractionalObjectDestroyed, IInteractionalObject>.Instance.Fire(this);
        }

    }
}
