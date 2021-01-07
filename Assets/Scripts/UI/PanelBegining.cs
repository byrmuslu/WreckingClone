namespace Base.UI
{
    using Base.Game.Signal;
    using UnityEngine;
    public class PanelBegining : MonoBehaviour
    {
        private void Awake()
        {
            SignalBus<SignalStartGame>.Instance.Register(OnStartGame);
        }

        private void OnDestroy()
        {
            SignalBus<SignalStartGame>.Instance.UnRegister(OnStartGame);
        }

        private void OnStartGame()
        {
            gameObject.SetActive(false);
        }
    }
}