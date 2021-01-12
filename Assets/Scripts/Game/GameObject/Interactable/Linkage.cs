namespace Base.Game.GameObject.Interactable
{
    using UnityEngine;

    public class Linkage : MonoBehaviour
    {
        private Vector3 _initLocalPos;
        private Vector3 _initLocalRot;

        private void Awake()
        {
            _initLocalPos = transform.localPosition;
            _initLocalRot = transform.localEulerAngles;
        }

        public void Init()
        {
            Active();
            transform.localPosition = _initLocalPos;
            transform.localRotation = Quaternion.Euler(_initLocalRot);
        }

        public void Active()
        {
            transform.localPosition = _initLocalPos;
            transform.localRotation = Quaternion.Euler(_initLocalRot);
            gameObject.SetActive(true);
        }

        public void DeActive()
        {
            gameObject.SetActive(false);
        }
    }
}