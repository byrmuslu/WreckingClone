namespace Base.Game.GameObject.Interactable
{
    using Base.Game.GameObject.Interactional;
    public interface IInteractableObject
    {
        void Interact(IInteractionalObject obj);
        UnityEngine.Transform GetTransform();
    }
}
