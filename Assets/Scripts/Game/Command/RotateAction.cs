namespace Base.Game.Command
{
    using UnityEngine;

    public class RotateAction
    {
        private IRotateableObject _obj;
        public RotateAction(IRotateableObject obj) => _obj = obj;
        public void RotateAxisX() => _obj.Transform.RotateAround(_obj.CenterPoint, Vector3.right, _obj.RotateSpeed);
        public void RotateAxisXMinus() => _obj.Transform.RotateAround(_obj.CenterPoint, Vector3.right * -1, _obj.RotateSpeed);
        public void RotateAxisY() => _obj.Transform.RotateAround(_obj.CenterPoint, Vector3.up, _obj.RotateSpeed);
        public void RotateAxisYMinus() => _obj.Transform.RotateAround(_obj.CenterPoint, Vector3.up * -1, _obj.RotateSpeed);
        public void RotateAxisZ() => _obj.Transform.RotateAround(_obj.CenterPoint, Vector3.forward, _obj.RotateSpeed);
        public void ROtateAxisZMinus() => _obj.Transform.RotateAround(_obj.CenterPoint, Vector3.forward * -1, _obj.RotateSpeed);

    }
}