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

        public float RotateSpeed { get; protected set; }
        public Vector3 CenterPoint { get => transform.position; }
        public Transform Transform { get => transform; }

        public Rigidbody Rigidbody { get; protected set; }
        public float Speed { get; protected set; }
        protected IBaseCarContext Context { get; set; }


        private void Awake()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Speed = _defaultSpeed * Time.fixedDeltaTime;
            RotateSpeed = _defaultRotateSpeed * Time.deltaTime;
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

        public virtual void Interact(IInteractableObject obj)
        {
            if(obj is IBall ball)
            {
                Rigidbody.AddForce(Vector3.right * ball.ImpactForce, ForceMode.Impulse);
                Rigidbody.AddForce(Vector3.up * ball.ImpactForce / 2, ForceMode.Impulse);
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
