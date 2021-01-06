namespace Base.Game.Command
{
    public interface IRotateableObject
    {
        UnityEngine.Transform Transform { get; }
        float RotateSpeed { get; }
        UnityEngine.Vector3 CenterPoint { get; }
    }
}