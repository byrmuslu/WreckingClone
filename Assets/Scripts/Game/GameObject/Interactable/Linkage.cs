namespace Base.Game.GameObject.Interactable
{
    using UnityEngine;

    public class Linkage : MonoBehaviour
    {
        private Vector3 _localPos;
        private Vector3 _localRot;

        public void Active()
        {
            transform.localPosition = _localPos;
            transform.localRotation = Quaternion.Euler(_localRot);
            gameObject.SetActive(true);
        }

        public void DeActive()
        {
            _localPos = transform.localPosition;
            _localRot = transform.localEulerAngles;
            gameObject.SetActive(false);
        }
    }
}