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

        private Coroutine _aiRoutine;

        protected override void Initialize()
        {
            base.Initialize();
        }

        public override void DeActive()
        {
            base.DeActive();
            SignalBus<SignalEnemyCarDestroyed, EnemyCar>.Instance.Fire(this);
        }

        private void OnDisable()
        {
            if(_aiRoutine != null)
            {
                StopCoroutine(_aiRoutine);
                _aiRoutine = null;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Invoke(nameof(StartAI), .1f);
        }

        private void StartAI()
        {
            _aiRoutine = StartCoroutine(AIAction());
        }

        private IEnumerator AIAction()
        {
            var wait = new WaitForFixedUpdate();
            float timer = _stateChangeTime -.5f;
            float rotateTime = UnityEngine.Random.Range(.25f, 3f);
            bool isRight = UnityEngine.Random.Range(0, 2) == 0;
            while (true)
            {
                timer += Time.fixedDeltaTime;
                if(timer >= _stateChangeTime)
                {
                    rotateTime = UnityEngine.Random.Range(.25f, 3f);
                    timer = 0;
                    isRight = UnityEngine.Random.Range(0, 2) == 0;
                }
                rotateTime -= Time.fixedDeltaTime;
                if (rotateTime > 0)
                {
                    if (isRight)
                        RotateRight();
                    else
                        RotateLeft();
                }
                Movement();
                yield return wait;
            }
        }

    }
}