namespace Base.Game.GameObject.Environment
{
    using Base.Game.GameObject.Interactable;
    using Base.Game.GameObject.Interactional;
    using Base.Game.Signal;
    using Base.Util;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class Carrier : MonoBehaviour, ICarrier, IInteractableObject
    {
        [SerializeField] private Transform _movedObjectTransform = null;
        [Space(20)]
        [SerializeField] private float _heightToFall = 5f;

        public Transform Transform => transform;

        public Vector3 MovedObjectPosition => _movedObjectTransform.position;

        private void OnEnable()
        {
            transform.position = new Vector3(Random.Range(Constant.platformRadius * -1, Constant.platformRadius), _heightToFall, Random.Range(Constant.platformRadius * -1, Constant.platformRadius));
        }

        public void Active()
        {
            gameObject.SetActive(true);
        }

        public void DeActive()
        {
            gameObject.SetActive(false);
            SignalBus<SignalCarrierDestroyed, Carrier>.Instance.Fire(this);
            SignalBus<SignalInteractableObjectDestroyed, IInteractableObject>.Instance.Fire(this);
        }

        public void Interact(IInteractionalObject obj)
        {
        }
    }
}
