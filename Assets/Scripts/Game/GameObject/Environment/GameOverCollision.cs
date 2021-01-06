namespace Base.Game.GameObject.Environment
{
    using Base.Game.GameObject.Interactable;
    using Base.Game.GameObject.Interactional;
    using UnityEngine;
    public class GameOverCollision : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<IInteractableObject>()?.DeActive();
            other.GetComponent<IInteractionalObject>()?.DeActive();
        }

        private void OnCollisionEnter(Collision collision)
        {
            collision.collider.GetComponent<IInteractableObject>()?.DeActive();
            collision.collider.GetComponent<IInteractionalObject>()?.DeActive();
        }
    }
}