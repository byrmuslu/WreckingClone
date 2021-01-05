namespace Base.Game.GameObject.Interactable
{
    using Base.Game.GameObject.Interactional;
    using Base.Game.Signal;
    using UnityEngine;
    public class MagicBox : MonoBehaviour, IInteractableObject
    {
        [SerializeField] private float _timer = 3f;
        public float Timer { get => _timer; }
        private void DeActive()
        {
            gameObject.SetActive(false);
        }

        public void Active()
        {
            SignalBus<SignalSpawnMagicBox, MagicBox>.Instance.Fire(this);
            gameObject.SetActive(true);
        }

        #region Implementations

        public Transform GetTransform()
        {
            return transform;
        }

        public void Interact(IInteractionalObject obj)
        {
            if (!(obj is BaseCar))
                return;
            obj.Interact(this);
            DeActive();
        }

        #endregion

    }
}