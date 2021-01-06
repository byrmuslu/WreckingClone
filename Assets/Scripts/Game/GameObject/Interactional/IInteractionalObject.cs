namespace Base.Game.GameObject.Interactional
{
    using Base.Game.GameObject.Interactable;
    public interface IInteractionalObject
    {
        void Interact(IInteractableObject obj);
        UnityEngine.Transform Transform { get; }
        void Active();
        void DeActive();
    }
}