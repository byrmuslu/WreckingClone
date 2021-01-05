namespace Base.Game.GameObject.Interactable
{
    using UnityEngine;

    public class Linkage : MonoBehaviour
    {
        public void Active()
        {
            gameObject.SetActive(true);
        }

        public void DeActive()
        {
            gameObject.SetActive(false);
        }
    }
}