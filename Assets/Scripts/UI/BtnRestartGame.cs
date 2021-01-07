namespace Base.UI
{
    using Base.Game.Signal;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    [RequireComponent(typeof(Button))]
    public class BtnRestartGame : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            SignalBus<SignalRestartGame>.Instance.Fire();
        }
    }
}