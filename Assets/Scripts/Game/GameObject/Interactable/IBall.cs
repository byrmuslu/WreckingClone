namespace Base.Game.GameObject.Interactional
{
    public interface IBall
    {
        float ImpactForce { get; }
        void AddForce();
        void ChangeState(float timer);
    }
}