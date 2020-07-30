// CPUの情報を統括するクラス
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarGeneral : MonoBehaviour
    {
        private CarState _state = null;     // 車の状態
        private CarController _carCtl;      // 車の操作

        private Vector3 _offset = new Vector3(0,1.5f,-4.5f);
        float _froDis = 50f;                        // 直線レイの長さ
        Vector3[] _wayDir = new Vector3[]           // 左右のレイの方向
        {
            new Vector3(-3f,0,-1f),         // 右
            new Vector3(3f,0,-1f),          // 左
            new Vector3(-1f,0,-4f),         // 斜め右
            new Vector3(1f,0,-4f),          // 斜め左
            new Vector3(-1f,0,-9f),         // 直線右
            new Vector3(1f,0,-9f)           // 直線左
        };
        float[] _wayDis = { 5.0f,30f,45f };         // 左右レイの長さ
        Vector3 _froDir = new Vector3(0,0,-1);      // まっすぐのレイ
        Vector3 _vertDir = new Vector3(0, -2f, 0);  // 垂直のレイ
        float[] _turnVol = { 0.4f, 0.35f, 0.15f };

        private void Awake()
        {
            _carCtl = GetComponent<CarController>();
        }
        public CarGeneral()
        {
            if (_state == null)
            {
                _state = new IdleState();
            }
        }
        void FixedUpdate()
        {
            CheckEnemy();
            var rad = new Random();
            float v = CheckFront();
            float h = CheckWay();
            float b = _state.GetBrake();
            _carCtl.Move(h, v, v, b);
        }
        float CheckWay()
        {
            float f = 0;
            for (int i = 0; i < _wayDir.Length; i++)
            {
                var pos = transform.TransformPoint(_offset);
                var way = transform.TransformDirection(_wayDir[i]);
                var vert = transform.TransformDirection(_vertDir);
                f += _state.IsHitWay(pos, way, vert, _wayDis[i / 2], i) * _turnVol[i / 2];
            }
            return f;
        }
        float CheckFront()
        {
            float f = 0;
            var pos = transform.TransformPoint(_offset);
            var way = transform.TransformDirection(_froDir);
            var vert = transform.TransformDirection(_vertDir);
            f += _state.IsHitFront(pos, way, vert, _froDis);
            return f;
        }
        void CheckEnemy()
        {
            Vector3[] vec = new Vector3[]
            {
                _froDir,
                _wayDir
            };
            for (int i = 0; i < vec.Length; i++)
            {
                var pos = transform.TransformPoint(_offset);
                var way = transform.TransformDirection(vec[i]);
                //_state.IsHitEnemy(pos, way, _wayDis[i / 2], i);
            }
        }
        CarState ChangeState()
        {
            return _state;
        }
    }
}
