namespace Base.Game.Command
{
    public interface IRotateableObject
    {
        UnityEngine.Transform GetTransform();
        float GetRotateSpeed();
        UnityEngine.Vector3 GetCenterPoint();
    }
}