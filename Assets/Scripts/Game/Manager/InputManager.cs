namespace Base.Game.Manager
{
    using Base.Game.Signal;
    using UnityEngine;

    public class InputManager : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick = null;
        void Update()
        {
            if (!_joystick)
                return;
            SignalBus<SignalHorizontalMultipier, float>.Instance.Fire(_joystick.Horizontal);
        }
    }
}
