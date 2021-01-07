namespace Base.Game.Environment
{
    using global::Base.Game.GameObject.Interactional;
    using global::Base.Game.Signal;
    using System;
    using System.Collections;
    using UnityEngine;

    namespace Base.Game.GameObject.Environment
    {
        public class BasicCameraController : MonoBehaviour
        {
            [SerializeField] private float _zOffset = 40f;
            private Transform _target;
            private void Awake()
            {
                SignalBus<SignalSpawnPlayerCar, PlayerCar>.Instance.Register(OnPlayerCarSpawned);
            }

            private void OnDestroy()
            {
                SignalBus<SignalSpawnPlayerCar, PlayerCar>.Instance.UnRegister(OnPlayerCarSpawned);
            }

            private void OnPlayerCarSpawned(PlayerCar obj)
            {
                _target = obj.transform;
            }

            private void LateUpdate()
            {
                if (_target == null)
                    return;
                transform.position = new Vector3(_target.position.x, transform.position.y, _target.position.z - _zOffset);
            }

        }
    }
}