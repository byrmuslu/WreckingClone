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
            SignalBus<SignalHorizontalMultipier, float>.Instance.Register(OnRotationChanged);
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
            SignalBus<SignalHorizontalMultipier, float>.Instance.UnRegister(OnRotationChanged);
        }

        private void OnRotationChanged(float h)
        {
            if (h == 1)
                RotateRight();
            else if (h == -1)
                RotateLeft();
        }

    }
}