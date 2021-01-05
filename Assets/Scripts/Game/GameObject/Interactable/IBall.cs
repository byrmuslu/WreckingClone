namespace Base.Game.GameObject.Interactional
{
    public interface IBall
    {
        float GetImpactForce();
        void AddForce();
        void ChangeState(float timer);
    }
}