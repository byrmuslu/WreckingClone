namespace Base.Game.GameObject.Environment
{
    public interface ICarrier
    {
        public UnityEngine.Transform Transform { get; }
        public UnityEngine.Vector3 MovedObjectPosition { get; }
        void Active();
        void DeActive();
    }
}