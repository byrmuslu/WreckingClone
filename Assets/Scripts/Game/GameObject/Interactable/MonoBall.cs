namespace Base.Game.GameObject.Interactable
{
    using Base.Game.GameObject.Interactional;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    public partial class Ball : MonoBehaviour
    {
        [SerializeField] private MeshFilter _meshFilter;
        [Space(10)]
        [SerializeField] private float _changedStateRotationSpeed = 200f;
        [Space(10)]
        [SerializeField] private float _defaultImpactForce = 10f;
        private float _impactForce;
        [SerializeField] private float _defaultRotationMultipier = 1f;
        private float _rotationMultipier;

        private List<Linkage> _linkages;
        private BaseCar _ownCar = null;

        private float _stateTime;
        private Coroutine _actionRoutine;
        private Vector3 _initLocalPos;
        private Rigidbody _body;
        private Collider _collider;
        private Joint _joint;

        private void Awake()
        {
            _joint = GetComponent<Joint>();
            _collider = GetComponent<Collider>();
            _body = GetComponent<Rigidbody>();
            _impactForce = _defaultImpactForce;
            _initLocalPos = transform.localPosition;
            _ownCar = GetComponentInParent<BaseCar>();
            _linkages = transform.parent.GetComponentsInChildren<Linkage>().ToList();
            _rotationMultipier = _defaultRotationMultipier;
        }

        public void Init()
        {
            transform.parent = _ownCar.transform;
            _collider.isTrigger = false;
            _body.isKinematic = false;
            transform.localPosition = _initLocalPos;
            _actionRoutine = null;
            foreach (Linkage linkage in _linkages)
                linkage.Init();
        }

        public void SetMesh(Mesh mesh)
        {
            _meshFilter.mesh = mesh;
        }

        private IEnumerator ChangedStateAction(float changedStateTime)
        {
            _impactForce = _defaultImpactForce * 2;
            _stateTime = changedStateTime;
            var wait = new WaitForFixedUpdate();
            float timer = 0f;
            Rigidbody connectedBody = _joint.connectedBody;
            foreach (Linkage linkage in _linkages)
                linkage.DeActive();
            transform.localPosition = _initLocalPos;
            _body.isKinematic = true;
            _collider.isTrigger = true;
            transform.parent = null;
            while (timer < _stateTime)
            {
                timer += Time.fixedDeltaTime;
                transform.RotateAround(_ownCar.transform.position, Vector3.up, _changedStateRotationSpeed * Time.fixedDeltaTime);
                transform.position = new Vector3(transform.position.x, _ownCar.transform.position.y, transform.position.z);
                yield return wait;
            }
            transform.parent = _ownCar.transform;
            _collider.isTrigger = false;
            _body.isKinematic = false;
            foreach (Linkage linkage in _linkages)
                linkage.Active();
            transform.localPosition = _initLocalPos;
            _joint.connectedBody = connectedBody;
            _actionRoutine = null;
            _impactForce = _defaultImpactForce;
        }

        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<IInteractableObject>()?.Interact(_ownCar);
        }

    }
}