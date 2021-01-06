namespace Base.Game.GameObject.Interactional
{
    using Base.Game.Signal;

    public class PlayerCar : BaseCar
    {

        private void FixedUpdate()
        {
            Movement();        
        }

        private void OnEnable()
        {
            Registration();
        }

        private void OnDisable()
        {
            UnRegistration();
        }

        private void Registration()
        {
            SignalBus<SignalTouch, bool>.Instance.Register(OnTouched);
        }

        private void UnRegistration()
        {
            SignalBus<SignalTouch, bool>.Instance.UnRegister(OnTouched);
        }

        private void OnTouched(bool touched)
        {
            if (!touched)
                return;
            Rotate();
        }

    }
}