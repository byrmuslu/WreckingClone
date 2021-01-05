namespace Base.Game.Command
{
    using UnityEngine;

    public class RotateAction
    {
        private IRotateableObject _obj;
        public RotateAction(IRotateableObject obj) => _obj = obj;
        public void RotateAxisX() => _obj.GetTransform().RotateAround(_obj.GetCenterPoint(), Vector3.right, _obj.GetRotateSpeed());
        public void RotateAxisXMinus() => _obj.GetTransform().RotateAround(_obj.GetCenterPoint(), Vector3.right * -1, _obj.GetRotateSpeed());
        public void RotateAxisY() => _obj.GetTransform().RotateAround(_obj.GetCenterPoint(), Vector3.up, _obj.GetRotateSpeed());
        public void RotateAxisYMinus() => _obj.GetTransform().RotateAround(_obj.GetCenterPoint(), Vector3.up * -1, _obj.GetRotateSpeed());
        public void RotateAxisZ() => _obj.GetTransform().RotateAround(_obj.GetCenterPoint(), Vector3.forward, _obj.GetRotateSpeed());
        public void ROtateAxisZMinus() => _obj.GetTransform().RotateAround(_obj.GetCenterPoint(), Vector3.forward * -1, _obj.GetRotateSpeed());

    }
}