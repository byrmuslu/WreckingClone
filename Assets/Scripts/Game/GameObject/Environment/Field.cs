namespace Base.Game.Environment
{
    using System.Collections;
    using UnityEngine;

    public class Field : MonoBehaviour
    {
        [SerializeField] private float _explosionTime = 2f;

        private Color _defaultColor;
        private Material _material;

        private void Awake()
        {
            _material = GetComponent<MeshRenderer>().material;
            _defaultColor = _material.color;
        }

        public void DeActive()
        {
            StopAllCoroutines();
            StartCoroutine(DeActiveAction());
        }

        private IEnumerator DeActiveAction()
        {
            var wait = new WaitForFixedUpdate();
            float timer = 0f;
            float colorChangeTimer = .5f;
            bool isDefault = true;
            transform.position -= Vector3.up * Time.fixedDeltaTime;
            while (timer < _explosionTime)
            {
                timer += Time.fixedDeltaTime;
                colorChangeTimer -= Time.fixedDeltaTime;
                if (timer > _explosionTime / 2)
                    transform.Translate(0, -1 * Time.fixedDeltaTime, 0);
                if(colorChangeTimer <= 0)
                {
                    colorChangeTimer = .5f;
                    isDefault = !isDefault;
                    _material.color = isDefault ? _defaultColor : Color.red;
                }
                yield return wait;
            }
            gameObject.SetActive(false);
        }

    }
}
