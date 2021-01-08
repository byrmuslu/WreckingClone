namespace Base.Game.Manager
{
    using Base.Game.Environment;
    using Base.Game.Factory;
    using Base.Game.GameObject.Environment;
    using Base.Game.GameObject.Interactable;
    using Base.Game.GameObject.Interactional;
    using Base.Game.Signal;
    using Base.Util;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class StageController : MonoBehaviour
    {
        [SerializeField] private List<Mesh> _carMeshes;
        [SerializeField] private List<Mesh> _ballMeshes;
        [Space(20)]
        [SerializeField] private float _fieldExplosionTime = 20f;
        [SerializeField] private List<Field> _defaultExplosiveZones = null;
        [Space(10)]
        [SerializeField] private int _enemyCount = 2;

        private IFactory<Carrier> _carrierFactory;
        private IFactory<MagicBox> _magicBoxFactory;
        private IFactory<PlayerCar> _playerCarFactory;
        private IFactory<EnemyCar> _enemyCarFactory;

        private List<Field> _explosiveZones;
        private List<IInteractableObject> _interactableObjectInGame;
        private List<IInteractionalObject> _interactionalObjectInGame;


        private Coroutine _fieldExplosionRoutine;

        private bool _gameOver = false;

        private void Awake()
        {
            Registration();
        }

        private void OnDestroy()
        {
            UnRegistration();
        }

        private void Registration()
        {
            SignalBus<SignalRestartGame>.Instance.Register(OnRestartGame);
            SignalBus<SignalStartGame>.Instance.Register(OnStartGame);
            SignalBus<SignalInteractableObjectDestroyed, IInteractableObject>.Instance.Register(OnInteractableObjectDestroyed);
            SignalBus<SignalInteractionalObjectDestroyed, IInteractionalObject>.Instance.Register(OnInteractionalObjectDestroyed);
        }

        private void OnInteractionalObjectDestroyed(IInteractionalObject obj)
        {
            if (_gameOver)
                return;
            if(obj is PlayerCar)
            {
                SignalBus<SignalGameOver, bool>.Instance.Fire(false);
                _gameOver = true;
            }
            if(obj is EnemyCar)
            {
                _enemyCount--;
                if(_enemyCount <= 0)
                {
                    SignalBus<SignalGameOver, bool>.Instance.Fire(true);
                    _gameOver = true;
                }
            }
            _interactionalObjectInGame.Remove(obj);
        }

        private void OnInteractableObjectDestroyed(IInteractableObject obj)
        {
            if (_gameOver)
                return;
            _interactableObjectInGame.Remove(obj);
        }

        private void UnRegistration()
        {
            SignalBus<SignalRestartGame>.Instance.UnRegister(OnRestartGame);
            SignalBus<SignalStartGame>.Instance.UnRegister(OnStartGame);
            SignalBus<SignalInteractableObjectDestroyed, IInteractableObject>.Instance.UnRegister(OnInteractableObjectDestroyed);
            SignalBus<SignalInteractionalObjectDestroyed, IInteractionalObject>.Instance.UnRegister(OnInteractionalObjectDestroyed);
        }

        private void OnRestartGame()
        {
            RestartStage();
        }

        private void OnStartGame()
        {
            Initialize();
        }

        private void Initialize()
        {
            _interactableObjectInGame = new List<IInteractableObject>();
            _interactionalObjectInGame = new List<IInteractionalObject>();
            _explosiveZones = new List<Field>();
            _explosiveZones.AddRange(_defaultExplosiveZones);
            _carrierFactory = new Factory<Carrier, SignalCarrierDestroyed>.Builder()
                              .SetPrefab("Carrier")
                              .SetHandle()
                              .Build();
            _magicBoxFactory = new Factory<MagicBox, SignalMagicBoxDestroyed>.Builder()
                               .SetPrefab("MagicBox")
                               .SetHandle()
                               .Build();
            _playerCarFactory = new Factory<PlayerCar, SignalPlayerCarDestroyed>.Builder()
                                .SetPrefab("PlayerCar")
                                .SetHandle()
                                .Build();
            _enemyCarFactory = new Factory<EnemyCar, SignalEnemyCarDestroyed>.Builder()
                               .SetPrefab("EnemyCar")
                               .SetHandle()
                               .Build();
            PlayerSpawn();
            EnemiesSpawn();
            MagicBoxSpawn();
            _fieldExplosionRoutine = StartCoroutine(FieldExplosionAction());
        }

        private void PlayerSpawn()
        {
            PlayerCar playerCar = _playerCarFactory.GetObject();
            playerCar.SetBallMesh(_ballMeshes[Random.Range(0, _ballMeshes.Count)]);
            playerCar.Active();
            _interactionalObjectInGame.Add(playerCar);
        }

        private void EnemiesSpawn()
        {
            for (int i = 0; i < _enemyCount; i++)
            {
                EnemyCar enemyCar = _enemyCarFactory.GetObject();
                enemyCar.SetBallMesh(_ballMeshes[Random.Range(0, _ballMeshes.Count)]);
                enemyCar.Active();
                _interactionalObjectInGame.Add(enemyCar);
            }
        }

        private void MagicBoxSpawn()
        {
            MagicBox magicBox = _magicBoxFactory.GetObject();
            Carrier carrier = _carrierFactory.GetObject();
            magicBox.Connect(carrier);
            carrier.Active();
            magicBox.Active();
            _interactableObjectInGame.Add(magicBox);
            _interactableObjectInGame.Add(carrier);
        }

        private void RestartStage()
        {
            if (_fieldExplosionRoutine != null)
                StopCoroutine(_fieldExplosionRoutine);
            for (int i = 0; i < _interactableObjectInGame.Count; i++)
                _interactableObjectInGame[i].DeActive();
            _interactableObjectInGame.Clear();
            for (int i = 0; i < _interactionalObjectInGame.Count; i++)
                _interactionalObjectInGame[i].DeActive();
            _interactionalObjectInGame.Clear();
            foreach (Field field in _defaultExplosiveZones)
                field.Active();
            _explosiveZones.Clear();
            _explosiveZones.AddRange(_defaultExplosiveZones);
            _enemyCount = 2;
            PlayerSpawn();
            EnemiesSpawn();
            MagicBoxSpawn();
            _fieldExplosionRoutine = StartCoroutine(FieldExplosionAction());
            _gameOver = false;
        }

        private IEnumerator FieldExplosionAction()
        {
            var wait = new WaitForFixedUpdate();
            float timer = _fieldExplosionTime;
            while(_explosiveZones.Count > 0)
            {
                timer -= Time.fixedDeltaTime;
                if(timer <= 0)
                {
                    Constant.platformRadius -= 25;
                    MagicBoxSpawn();
                    MagicBoxSpawn();
                    timer = _fieldExplosionTime;
                    Field explosiveZone = _explosiveZones[0];
                    explosiveZone.DeActive();
                    _explosiveZones.RemoveAt(0);
                }
                yield return wait;
            }
            _fieldExplosionRoutine = null;
        }

    }
}
