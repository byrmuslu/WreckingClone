namespace Base.Game.GameObject.Interactional
{
    using Base.Game.GameObject.Interactable;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    [RequireComponent(typeof(Collider), typeof(Rigidbody), typeof(HingeJoint))]
    public class Ball : MonoBehaviour, IInteractableObject, IBall
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

        public Transform Transform { get; }

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

        public void AddForce()
        {
            _body?.AddForce(Vector3.right * _rotationMultipier, ForceMode.Impulse);
        }

        private IEnumerator ChangedStateAction(float changedStateTime)
        {
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
            while(timer < changedStateTime)
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
        }

        #region Implementations
        
        public void Interact(IInteractionalObject obj)
        {
            if (obj.Equals(_ownCar))
                return;
            obj.Interact(this);
        }

        public float GetImpactForce()
        {
            return _impactForce;
        }

        public void ChangeState(float timer)
        {
            StopAllCoroutines();
            StartCoroutine(ChangedStateAction(timer));
        }

        public void Active()
        {
            gameObject.SetActive(true);
        }

        public void DeActive()
        {
            gameObject.SetActive(false);
        }

        #endregion
    }
}