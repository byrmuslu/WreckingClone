namespace Base.Game.Command
{
    public interface IMoveableObject
    {
        UnityEngine.Rigidbody Rigidbody { get; }
        UnityEngine.Transform Transform { get; }
        float Speed { get; }
    }
}