namespace Base.Game.GameObject.Interactional
{
    using Base.Game.Command;
    using Base.Game.GameObject.Interactable;
    using Base.Game.State;
    using UnityEngine;

    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public abstract class BaseCar : MonoBehaviour, IInteractionalObject, IMoveableObject, IRotateableObject
    {
        [SerializeField] private float _defaultSpeed = 1f;
        [SerializeField] private float _defaultRotateSpeed = 1f;
        [Space(20)]
        [SerializeField] protected Ball _connectedBall;

        protected float _speed;
        protected float _rotateSpeed;

        protected Rigidbody _body;

        protected IBaseCarContext Context { get; set; }

        private void Awake()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            _body = GetComponent<Rigidbody>();
            _speed = _defaultSpeed * Time.fixedDeltaTime;
            _rotateSpeed = _defaultRotateSpeed * Time.deltaTime;
            Context = new BaseCarContext(this);
            Context.State = new StateMoveForwardRotateAxisY((BaseCarContext)Context);
        }

        protected virtual void Movement()
        {
            Context.Move?.Execute();
        }

        protected virtual void Rotate()
        {
            Context.Rotate?.Execute();
            _connectedBall.AddForce();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            other.GetComponent<IInteractableObject>()?.Interact(this);
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            collision.collider.GetComponent<IInteractableObject>()?.Interact(this);
        }

        #region Implementations

        public virtual void Interact(IInteractableObject obj)
        {
            if(obj is IBall ball)
            {
                _body.AddForce(Vector3.right * ball.GetImpactForce(), ForceMode.Impulse);
                _body.AddForce(Vector3.up * ball.GetImpactForce()/2, ForceMode.Impulse);
            }
            if (obj is MagicBox box)
                _connectedBall.ChangeState(box.Timer);
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public Rigidbody GetRigidbody()
        {
            return _body;
        }

        public float GetSpeed()
        {
            return _speed;
        }

        public float GetRotateSpeed()
        {
            return _rotateSpeed;
        }

        public Vector3 GetCenterPoint()
        {
            return transform.position;
        }

        #endregion
    }
}
