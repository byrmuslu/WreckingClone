namespace Base.Game.GameObject.Interactional
{
    using Base.Game.Command;
    using Base.Game.GameObject.Interactable;
    using Base.Util;
    using UnityEngine;

    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public partial class BaseCar : MonoBehaviour
    {
        [SerializeField] private float _defaultSpeed = 1f;
        [SerializeField] private float _defaultRotateSpeed = 1f;
        [Space(20)]
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] protected Ball _connectedBall;


        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Speed = _defaultSpeed * Time.fixedDeltaTime;
            RotateSpeed = _defaultRotateSpeed * Time.deltaTime;
            Initialize();
        }


        protected virtual void OnEnable()
        {
            float value = Constant.platformRadius / 2;
            transform.position = new Vector3(Random.Range(-value, value), 5f, Random.Range(-value, value));
        }

        public void SetMesh(Mesh mesh)
        {
            _meshFilter.mesh = mesh;
        }

        public void SetBallMesh(Mesh mesh)
        {
            _connectedBall.SetMesh(mesh);
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