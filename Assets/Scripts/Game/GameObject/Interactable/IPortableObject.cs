namespace Base.Game.GameObject.Interactable
{
    using Base.Game.GameObject.Environment;

    public interface IPortableObject
    {
        public UnityEngine.Transform Transform { get; }
        void Connect(ICarrier carrier);
    }
}