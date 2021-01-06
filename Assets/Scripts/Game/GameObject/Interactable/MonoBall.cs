namespace Base.Game.GameObject.Interactable
{
    using Base.Game.GameObject.Interactional;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    public partial class Ball : MonoBehaviour
    {

        [SerializeField] private float _changedStateRotationSpeed = 200f;
        [Space(10)]
        [SerializeField] private float _impactForce = 10f;
        [SerializeField] private float _defaultRotationMultipier = 1f;
        private float _rotationMultipier;

        private Collider _collider;
        private Rigidbody _body;
        private List<Linkage> _linkages;
        private BaseCar _ownCar = null;
        private HingeJoint _joint;

        private float _stateTime;
        private Coroutine _actionRoutine;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _joint = GetComponent<HingeJoint>();
            _ownCar = GetComponentInParent<BaseCar>();
            _linkages = _ownCar.GetComponentsInChildren<Linkage>().ToList();
            _body = GetComponent<Rigidbody>();
            _rotationMultipier = _defaultRotationMultipier;
            _changedStateRotationSpeed *= Time.fixedDeltaTime;
        }

        private IEnumerator ChangedStateAction(float changedStateTime)
        {
            _stateTime = changedStateTime;
            var wait = new WaitForFixedUpdate();
            float timer = 0f;
            _rotationMultipier = 0;
            Rigidbody connectedBody = _joint.connectedBody;
            _collider.isTrigger = true;
            _body.isKinematic = true;
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
            foreach (Linkage linkage in _linkages)
                linkage.Active();
            _collider.isTrigger = false;
            _body.isKinematic = false;
            transform.localPosition = locPos;
            transform.localRotation = Quaternion.Euler(locRot);
            _joint.connectedBody = connectedBody;
            _rotationMultipier = _defaultRotationMultipier;
            _actionRoutine = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<IInteractableObject>()?.Interact(_ownCar);
        }

    }
}