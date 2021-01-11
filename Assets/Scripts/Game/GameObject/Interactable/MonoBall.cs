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
        private Vector3 _targetPos;
        

        private void Awake()
        {
            _targetPos = transform.parent.localPosition + Vector3.forward * -3f;
            _impactForce = _defaultImpactForce;
            _initLocalPos = transform.localPosition;
            _ownCar = GetComponentInParent<BaseCar>();
            _linkages = transform.parent.GetComponentsInChildren<Linkage>().ToList();
            _rotationMultipier = _defaultRotationMultipier;
            _changedStateRotationSpeed *= Time.fixedDeltaTime;
        }


        public void Init()
        {
            _impactForce = _defaultImpactForce;
            transform.localPosition = _initLocalPos;
            _rotationMultipier = _defaultRotationMultipier;
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
            _rotationMultipier = 0;
            Vector3 locPos = transform.localPosition;
            Vector3 locRot = transform.localEulerAngles;
            foreach (Linkage linkage in _linkages)
                linkage.DeActive();
            while (timer < _stateTime)
            {
                timer += Time.fixedDeltaTime;
                transform.RotateAround(_ownCar.transform.position, Vector3.up, _changedStateRotationSpeed);
                yield return wait;
            }
            transform.localPosition = locPos;
            transform.localRotation = Quaternion.Euler(locRot);
            foreach (Linkage linkage in _linkages)
                linkage.Active();
            _rotationMultipier = _defaultRotationMultipier;
            _actionRoutine = null;
            _impactForce = _defaultImpactForce;
        }

        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<IInteractableObject>()?.Interact(_ownCar);
        }

    }
}