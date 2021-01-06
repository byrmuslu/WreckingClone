namespace Base.Game.Manager
{
    using Base.Game.Environment;
    using Base.Game.Factory;
    using Base.Game.GameObject.Environment;
    using Base.Game.GameObject.Interactable;
    using Base.Game.Signal;
    using Base.Util;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class StageController : MonoBehaviour
    {
        [SerializeField] private float _fieldExplosionTime = 20f;
        [SerializeField] private List<Field> _explosiveZones = null;
        [Space(10)]
        [SerializeField] private Carrier _carrierPrefab = null;
        [SerializeField] private MagicBox _magicBoxPrefab = null;

        private IFactory<Carrier> _carrierFactory;
        private IFactory<MagicBox> _magicBoxFactory;


        private void Awake()
        {
            _carrierFactory = new Factory<Carrier, SignalCarrierDestroyed>.Builder()
                              .SetPrefab(_carrierPrefab)
                              .SetHandle()
                              .Build();
            _magicBoxFactory = new Factory<MagicBox, SignalMagicBoxDestroyed>.Builder()
                               .SetPrefab(_magicBoxPrefab)
                               .SetHandle()
                               .Build();

            _magicBoxFactory.GetObject().Connect(_carrierFactory.GetObject());
            StartCoroutine(FieldExplosionAction());
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
                    Constant.platformRadius -= 20;
                    _magicBoxFactory.GetObject().Connect(_carrierFactory.GetObject());
                    timer = _fieldExplosionTime;
                    Field explosiveZone = _explosiveZones[0];
                    explosiveZone.DeActive();
                    _explosiveZones.RemoveAt(0);
                }
                yield return wait;
            }
        }

    }
}
