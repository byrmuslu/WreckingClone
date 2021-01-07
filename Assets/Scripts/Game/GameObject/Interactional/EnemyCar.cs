namespace Base.Game.GameObject.Interactional
{
    using Base.Game.GameObject.Interactable;
    using Base.Game.Signal;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class EnemyCar : BaseCar
    {
        [SerializeField] private float _stateChangeTime = 5f;
        private Queue<MagicBox> _spawnedBoxes;

        protected override void Initialize()
        {
            base.Initialize();
            _spawnedBoxes = new Queue<MagicBox>();
        }

        public override void DeActive()
        {
            base.DeActive();
            SignalBus<SignalEnemyCarDestroyed, EnemyCar>.Instance.Fire(this);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            UnRegistration();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            StartCoroutine(AIAction());
            Registration();
        }

        private IEnumerator AIAction()
        {
            var wait = new WaitForFixedUpdate();
            float timer = _stateChangeTime -.5f;
            float rotateTime = UnityEngine.Random.Range(.25f, 3f);
            while (true)
            {
                timer += Time.fixedDeltaTime;
                if(timer >= _stateChangeTime)
                {
                    rotateTime = UnityEngine.Random.Range(.25f, 3f);
                    timer = 0;
                }
                rotateTime -= Time.fixedDeltaTime;
                if (rotateTime > 0)
                {
                    Rotate();
                }
                Movement();
                yield return wait;
            }
        }

        private void OnMagicBoxSpawned(MagicBox obj)
        {
            if (_spawnedBoxes.Contains(obj))
                return;
            _spawnedBoxes.Enqueue(obj);
        }

        private void Registration()
        {
            SignalBus<SignalSpawnMagicBox, MagicBox>.Instance.Register(OnMagicBoxSpawned);
        }

        private void UnRegistration()
        {
            SignalBus<SignalSpawnMagicBox, MagicBox>.Instance.UnRegister(OnMagicBoxSpawned);
        }

    }
}