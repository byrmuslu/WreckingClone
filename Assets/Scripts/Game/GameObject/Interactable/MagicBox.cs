namespace Base.Game.GameObject.Interactable
{
    using Base.Game.GameObject.Environment;
    using Base.Game.GameObject.Interactional;
    using Base.Game.Signal;
    using UnityEngine;

    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class MagicBox : MonoBehaviour, IInteractableObject, IPortableObject
    {
        [SerializeField] private float _timer = 3f;
        public float Timer { get => _timer; }
        public Transform Transform { get => transform; }

        private bool _onGround = true;

        private ICarrier _connectedCarrier;

        public void DeActive()
        {
            gameObject.SetActive(false);
        }

        public void Active()
        {
            SignalBus<SignalSpawnMagicBox, MagicBox>.Instance.Fire(this);
            gameObject.SetActive(true);
        }

        public void Interact(IInteractionalObject obj)
        {
            if (!(obj is BaseCar) || !_onGround)
                return;
            obj.Interact(this);
            DeActive();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Carrier>())
                return;
            _onGround = true;
            _connectedCarrier?.DeActive();
            transform.parent = null;
            _connectedCarrier = null;
        }

        public void Connect(ICarrier carrier)
        {
            _onGround = false;
            _connectedCarrier = carrier;
            transform.position = carrier.MovedObjectPosition;
            transform.parent = carrier.Transform;
        }
    }
}