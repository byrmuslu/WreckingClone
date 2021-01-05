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
        [SerializeField] private Transform _baseTransform = null;
        [SerializeField] private float _changedStateRotationSpeed = 200f;
        [Space(10)]
        [SerializeField] private float _impactForce = 10f;
        [SerializeField] private float _defaultRotationMultipier = 1f;
        private float _rotationMultipier;


        private Rigidbody _body;
        private List<Linkage> _linkages;
        private BaseCar _ownCar = null;
        private HingeJoint _joint;


        private void Awake()
        {
            _joint = GetComponent<HingeJoint>();
            _ownCar = GetComponentInParent<BaseCar>();
            _linkages = _ownCar.GetComponentsInChildren<Linkage>().ToList();
            _body = GetComponent<Rigidbody>();
            _rotationMultipier = _defaultRotationMultipier;
            _changedStateRotationSpeed *= Time.fixedDeltaTime;
        }

        public void AddForce()
        {
            _body.AddForce(Vector3.right * _rotationMultipier, ForceMode.Impulse);
        }

        private IEnumerator ChangedStateAction(float changedStateTime)
        {
            var wait = new WaitForFixedUpdate();
            float timer = 0f;
            _rotationMultipier = 0;
            Rigidbody connectedBody = _joint.connectedBody;
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
            transform.position = _baseTransform.position;
            transform.rotation = _baseTransform.rotation;
            _joint.connectedBody = connectedBody;
            _rotationMultipier = _defaultRotationMultipier;
        }

        #region Implementations
        
        public Transform GetTransform()
        {
            return transform;
        }

        public void Interact(IInteractionalObject obj)
        {
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

        #endregion
    }
}