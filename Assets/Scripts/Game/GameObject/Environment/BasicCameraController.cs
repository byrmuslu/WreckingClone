namespace Base.Game.Environment
{
    using global::Base.Game.GameObject.Interactional;
    using global::Base.Game.Signal;
    using UnityEngine;

    namespace Base.Game.GameObject.Environment
    {
        public class BasicCameraController : MonoBehaviour
        {
            private Transform _target;
            private void Awake()
            {
                _target = FindObjectOfType<PlayerCar>().transform;
            }

            private void LateUpdate()
            {
                if (_target == null)
                    return;
                transform.position = new Vector3(_target.position.x, transform.position.y, _target.position.z);
            }

        }
    }
}