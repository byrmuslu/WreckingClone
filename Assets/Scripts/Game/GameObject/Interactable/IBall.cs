namespace Base.Game.GameObject.Interactional
{
    public interface IBall
    {
        UnityEngine.Transform Transform { get; }
        float ImpactForce { get; }
        void AddForce();
        void ChangeState(float timer);
    }
}