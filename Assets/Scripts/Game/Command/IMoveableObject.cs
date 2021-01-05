namespace Base.Game.Command
{
    public interface IMoveableObject
    {
        UnityEngine.Rigidbody GetRigidbody();
        float GetSpeed();
    }
}