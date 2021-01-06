namespace Base.Game.Command
{
    public interface IMoveableObject
    {
        UnityEngine.Rigidbody Rigidbody { get; }
        float Speed { get; }
    }
}