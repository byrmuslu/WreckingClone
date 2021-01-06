namespace Base.Game.GameObject.Interactable
{
    using Base.Game.GameObject.Interactional;
    using UnityEngine;

    [RequireComponent(typeof(Collider), typeof(Rigidbody), typeof(HingeJoint))]
    public partial class Ball : IInteractableObject, IBall
    {
        public float ImpactForce { get => _impactForce; }
        public Transform Transform { get => transform; }

        public void AddForce()
        {
            _body?.AddForce(Vector3.right * _rotationMultipier, ForceMode.Impulse);
        }

        public void Interact(IInteractionalObject obj)
        {
            if (obj.Equals(_ownCar))
                return;
            obj.Interact(this);
        }

        public void ChangeState(float timer)
        {
            if(_actionRoutine != null || !gameObject.activeSelf)
            {
                _stateTime += timer;
                return;
            }
            _actionRoutine = StartCoroutine(ChangedStateAction(timer));
        }

        public void Active()
        {
            gameObject.SetActive(true);
        }

        public void DeActive()
        {
            gameObject.SetActive(false);
        }

    }
}