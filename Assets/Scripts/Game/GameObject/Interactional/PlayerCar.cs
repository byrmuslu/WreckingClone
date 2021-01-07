namespace Base.Game.GameObject.Interactional
{
    using Base.Game.Signal;
    using System;

    public class PlayerCar : BaseCar
    {
        private bool _canMove = true;
        private void FixedUpdate()
        {
            if (!_canMove)
                return;
            Movement();        
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _canMove = true;
            Registration();
            SignalBus<SignalSpawnPlayerCar, PlayerCar>.Instance.Fire(this);
        }

        public override void DeActive()
        {
            base.DeActive();
            SignalBus<SignalPlayerCarDestroyed, PlayerCar>.Instance.Fire(this);
        }

        private void OnDisable()
        {
            UnRegistration();
        }

        private void Registration()
        {
            SignalBus<SignalTouch, bool>.Instance.Register(OnTouched);
            SignalBus<SignalGameOver, bool>.Instance.Register(OnGameOver);
        }

        private void OnGameOver(bool obj)
        {
            _canMove = !obj;
            if(!_canMove)
                Rigidbody.velocity = UnityEngine.Vector3.zero;
        }

        private void UnRegistration()
        {
            SignalBus<SignalGameOver, bool>.Instance.UnRegister(OnGameOver);
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