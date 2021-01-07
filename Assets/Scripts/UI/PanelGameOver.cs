namespace Base.UI
{
    using Base.Game.Signal;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class PanelGameOver : MonoBehaviour
    {
        [SerializeField] private Text _text;

        private void Awake()
        {
            SignalBus<SignalGameOver, bool>.Instance.Register(OnGameOver);
            SignalBus<SignalRestartGame>.Instance.Register(OnRestartGame);
            DeActive();
        }

        private void OnRestartGame()
        {
            DeActive();
        }

        private void OnDestroy()
        {
            SignalBus<SignalRestartGame>.Instance.UnRegister(OnRestartGame);
            SignalBus<SignalGameOver, bool>.Instance.UnRegister(OnGameOver);
        }

        private void OnGameOver(bool obj)
        {
            if (obj)
            {
                _text.text = "You Win!!";
            }
            else
            {
                _text.text = "You Lose!!";
            }
            Active();
        }

        private void Active()
        {
            gameObject.SetActive(true);
        }

        private void DeActive()
        {
            gameObject.SetActive(false);
        }
    }
}