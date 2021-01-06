namespace Base.Game.GameObject.Interactional
{
    using Base.Game.Command;
    using Base.Game.GameObject.Interactable;
    using UnityEngine;

    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public partial class BaseCar : MonoBehaviour
    {
        [SerializeField] private float _defaultSpeed = 1f;
        [SerializeField] private float _defaultRotateSpeed = 1f;
        [Space(20)]
        [SerializeField] protected Ball _connectedBall;


        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Speed = _defaultSpeed * Time.fixedDeltaTime;
            RotateSpeed = _defaultRotateSpeed * Time.deltaTime;
            Initialize();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            other.GetComponent<IInteractableObject>()?.Interact(this);
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            collision.collider.GetComponent<IInteractableObject>()?.Interact(this);
        }

    }
}