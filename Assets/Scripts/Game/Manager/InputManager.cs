namespace Base.Game.Manager
{
    using Base.Game.Signal;
    using UnityEngine;

    public class InputManager : MonoBehaviour
    {
        void Update()
        {
            SignalBus<SignalTouch, bool>.Instance.Fire(Input.GetMouseButton(0));
        }
    }
}
