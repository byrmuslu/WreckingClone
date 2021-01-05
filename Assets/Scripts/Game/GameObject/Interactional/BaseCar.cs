namespace Base.Game.GameObject.Interactional
{
    using Base.Game.GameObject.Interactable;
    using UnityEngine;

    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public abstract class BaseCar : MonoBehaviour, IInteractionalObject
    {
        [SerializeField] private float _defaultSpeed = 1f;
        [SerializeField] private float _defaultRotateSpeed = 1f;
        [Space(20)]
        [SerializeField] protected Ball _connectedBall;

        protected float _speed;
        protected float _rotateSpeed;

        protected Rigidbody _body;

        private void Awake()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            _body = GetComponent<Rigidbody>();
            _speed = _defaultSpeed * Time.fixedDeltaTime;
            _rotateSpeed = _defaultRotateSpeed * Time.deltaTime;
        }

        protected virtual void Movement()
        {
            _body.velocity = transform.forward * _speed;
        }

        protected virtual void Rotate()
        {
            transform.Rotate(transform.up, _rotateSpeed);
            _connectedBall.AddForce();
        }

        protected virtual void FixedUpdate()
        {
            Movement();
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
                _body.AddForce(Vector3.up * ball.GetImpactForce(), ForceMode.Impulse);
            }
            if (obj is MagicBox box)
                _connectedBall.ChangeState(box.Timer);
        }

        public Transform GetTransform()
        {
            return transform;
        }

        #endregion
    }
}
