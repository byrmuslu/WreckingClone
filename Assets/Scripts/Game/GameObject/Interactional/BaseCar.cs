namespace Base.Game.GameObject.Interactional
{
    using Base.Game.Command;
    using Base.Game.GameObject.Interactable;
    using UnityEngine;

    public partial class BaseCar : IInteractionalObject, IMoveableObject, IRotateableObject
    {
        public float RotateSpeed { get; protected set; }
        public Vector3 CenterPoint { get => transform.position; }
        public Transform Transform { get => transform; }

        public Rigidbody Rigidbody { get; protected set; }
        public float Speed { get; protected set; }

        private ICommand _move;
        private ICommand _rotate;

        protected virtual void Initialize()
        {
            var moveAction = new MovementAction(this);
            _move = new Command<MovementAction>(moveAction, m => m.MoveForward());
            var rotateAction = new RotateAction(this);
            _rotate = new Command<RotateAction>(rotateAction, r => r.RotateAxisY());
        }

        protected virtual void Movement()
        {
            _move.Execute();
        }

        protected virtual void Rotate()
        {
            _rotate.Execute();
            _connectedBall.AddForce();
        }

        public virtual void Interact(IInteractableObject obj)
        {
            if(obj is IBall ball)
            {
                Rigidbody.AddExplosionForce(ball.ImpactForce, ball.Transform.position, 5f,3.0f);
            }
            if (obj is MagicBox box)
                _connectedBall.ChangeState(box.Timer);
        }

        public virtual void Active()
        {
            gameObject.SetActive(true);
        }

        public virtual void DeActive()
        {
            gameObject.SetActive(false);
        }

    }
}
