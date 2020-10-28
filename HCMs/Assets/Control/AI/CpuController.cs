using UnityEngine;
using System;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    class CpuController : MonoBehaviour
    {
        private CarController _carCtl;      // 車操作のクラス

        private Vector3 _offset = new Vector3(0, 1.5f, 4.5f);
        float _froDis = 45f;                    // 直線レイの長さ
        float[] _wayDis = { 45f, 30f, 5.0f };   // 左右レイの長さ


        Vector3 _froDir = new Vector3(0, 0, 1); // 直線レイ
        Vector3[] _wayDir = new Vector3[]
        {   new Vector3(-1f,0,9f),          // 直線右
            new Vector3(1f,0,9f) ,          // 直線左
            new Vector3(-1f,0,4f),          // 斜め右
            new Vector3(1f,0,4f),           // 斜め左
            new Vector3(-3f,0,1f),          // 右
            new Vector3(3f,0,1f)            // 左
        };

        Vector3[] _way = new Vector3[]
        {   new Vector3(-1f,0,20f).normalized,          // 直線右
            new Vector3(1f,0,20f).normalized ,          // 直線左
        };

        Vector3 _vertDir = new Vector3(0, -1f, 0);  // 垂直のレイ
        float[] _turnVol = { 0.15f, 0.3f, 0.38f };

        private float _speed = 0f;

        private void Awake()
        {
            _carCtl = GetComponent<CarController>();
        }

        void FixedUpdate()
        {
            // 認知・判断
            float v = CheckFront();
            float h = CheckWay();
            var sp = _carCtl.CurrentSpeed;

            FrontLimit();
            var l = LeftLimit();
            var r = RightLimit();

            if (sp >= 90)
            {
                v = 0;
            }
            if (Math.Abs(h) > 0)
            {
                v = -0.05f;
            }
            else
            {
                if (l - r > 0)
                {
                    h = 0.01f;
                    Debug.Log("右");
                }
                else if (l - r < 0)
                {
                    h = -0.01f;
                    Debug.Log("左");
                }
                else { }
            }
            // 操作          
            _carCtl.Move(h, v, v, 0);
        }


        float CheckWay()
        {
            float volume = 0;
            RaycastHit ray;
            // 左右の判定でハンドルの値を決める
            for (int i = 0; i < _wayDir.Length; i++)
            {
                var pos = transform.TransformPoint(_offset);
                var way = transform.TransformDirection(_wayDir[i]);
                var vert = transform.TransformDirection(_vertDir);
                float move = 0;
                pos += way.normalized * _wayDis[i / 2];
                if (Physics.Raycast(pos, vert, out ray, 3))
                {
                    move = i % 2 * 2 - 1f;
                }
                DebugDraw(transform.TransformPoint(_offset), way, _wayDis[i / 2], ray.collider);
                DebugDraw(pos, vert, 3, ray.collider);
                volume += move * _turnVol[i / 2];
            }
            return volume;
        }

        float CheckFront()
        {
            float ret = 0;
            RaycastHit ray;
            var pos = transform.TransformPoint(_offset);
            var way = transform.TransformDirection(_froDir);
            var vert = transform.TransformDirection(_vertDir);
            Physics.Raycast(pos, way, out ray, _froDis);
            
            pos += way.normalized * _froDis;
            // 前方の地面判定がある時
            if (Physics.Raycast(pos, vert, out ray, 3))
            {
                _speed += _speed < 1.0f ? 0.1f : 0;
            }
            DebugDraw(transform.TransformPoint(_offset), way, _froDis, ray.collider);
            DebugDraw(pos, vert, 3, ray.collider);
            ret += _speed;
            return ret;
        }

        void FrontLimit()
        {
            RaycastHit ray;
            var carPos = transform.TransformPoint(_offset);
            var way = transform.TransformDirection(_froDir);
            var vert = transform.TransformDirection(_vertDir);
            var pos = way.normalized * _froDis + carPos;
            var dis = _froDis;
            while (Physics.Raycast(pos, vert, out ray, 3))
            {
                dis += 0.1f;
                pos = carPos + way.normalized * dis;
            }
            Debug.DrawRay(pos, vert * 3, new Color(0, 0, 255f));
        }
        float RightLimit()
        {
            RaycastHit ray;
            var carPos = transform.TransformPoint(_offset);
            var way = transform.TransformDirection(_way[0]);
            var vert = transform.TransformDirection(_vertDir);
            var pos = way.normalized * _wayDis[0] + carPos;
            var dis = _wayDis[0];
            while (Physics.Raycast(pos, vert, out ray, 3))
            {
                dis += 0.1f;
                pos = carPos + way.normalized * dis;
            }
            Debug.DrawRay(pos, vert * 3, new Color(0,0,0));
            return dis;
        }
        float LeftLimit()
        {
            RaycastHit ray;
            var carPos = transform.TransformPoint(_offset);
            var way = transform.TransformDirection(_way[1]);
            var vert = transform.TransformDirection(_vertDir);
            var pos = way.normalized * _wayDis[0] + carPos;
            var dis = _wayDis[0];
            while (Physics.Raycast(pos, vert, out ray, 3))
            {
                dis += 0.1f;
                pos = carPos + way.normalized * dis;
            }
            Debug.DrawRay(pos, vert * 3, new Color(255f, 255f, 255f));
            return dis;
        }
        // シーン用レイ描画
        public void DebugDraw(Vector3 vec1, Vector3 vec2, float f, Collider col)
        {
            Color color = col ? Color.red : Color.green;
            Debug.DrawRay(vec1, vec2.normalized * f, color, 0, false);
        }
    }
}