namespace Base.Game.Manager
{
    using Base.Game.Environment;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class StageController : MonoBehaviour
    {
        [SerializeField] private float _fieldExplosionTime = 20f;
        [SerializeField] private List<Field> _explosiveZones = null;

        private void Awake()
        {
            StartCoroutine(ControllerAction());
        }

        private IEnumerator ControllerAction()
        {
            var wait = new WaitForFixedUpdate();
            float timer = _fieldExplosionTime;
            while(_explosiveZones.Count > 0)
            {
                timer -= Time.fixedDeltaTime;
                if(timer <= 0)
                {
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
