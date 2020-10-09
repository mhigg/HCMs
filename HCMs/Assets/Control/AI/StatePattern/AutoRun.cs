using UnityEngine;
using System;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class AutoRun : MonoBehaviour
    {
        private CarController _carCtl;      // 車の操作

        public GameObject[] _target;

        // レイキャストに使う変数
        public RaycastHit[] _hitW = new RaycastHit[6];
        public RaycastHit _hitF = new RaycastHit();
        private Vector3 _offset = new Vector3(0, 1.5f, 4.5f);
        float _froDis = 45f;                        // 直線レイの長さ
        Vector3 _froDir = new Vector3(0, 0, 1);       // 直線レイ

        float[] _wayDis = { 5.0f, 30f, 45f };         // 左右レイの長さ

        Vector3[] _wayDir = new Vector3[]           // 左右レイの方向
        {
            new Vector3(-3f,0,1f),         // 右
            new Vector3(3f,0,1f),          // 左
            new Vector3(-1f,0,4f),         // 斜め右
            new Vector3(1f,0,4f),          // 斜め左
            new Vector3(-1f,0,9f),         // 直線右
            new Vector3(1f,0,9f)           // 直線左
        };

        Vector3 _vertDir = new Vector3(0, -2f, 0);  // 垂直のレイ
        float[] _turnVol = { 0.4f, 0.35f, 0.15f };

        private float _speed = 0f;

        public AutoRun()
        {

        }

        private void Awake()
        {
            _carCtl = GetComponent<CarController>();
        }
        void FixedUpdate()
        {
            CheckEnemy();
            var rad = new UnityEngine.Random();
            float v = CheckFront();
            float h = CheckWay();
            var sp = _carCtl.CurrentSpeed;

            if(Math.Abs(h) > 0)
            {
                v = -0.05f;
            }
            if (sp >= 80)
            {
                v = 0;
            }
            _carCtl.Move(h, v, v, 0);
        }
        float CheckWay()
        {
            float volume = 0;


            for (int i = 0; i < _wayDir.Length; i++)
            {
                var pos = transform.TransformPoint(_offset);
                var way = transform.TransformDirection(_wayDir[i]);
                var vert = transform.TransformDirection(_vertDir);

                float move = 0;
                Physics.Raycast(pos, way, out _hitW[i], _wayDis[i / 2]);
                pos += way.normalized * _wayDis[i / 2];
                if (Physics.Raycast(pos, vert, out _hitW[i], 3))
                {
                    move = i % 2 * 2 - 1f;
                }
                DebugDraw(pos, vert, 3, _hitW[i].collider);
                volume += move * _turnVol[i / 2]; ;
            }


            return volume;
        }
        float CheckFront()
        {
            float ret = 0;
            var pos = transform.TransformPoint(_offset);
            var way = transform.TransformDirection(_froDir);
            var vert = transform.TransformDirection(_vertDir);

            float f = 0;
            Physics.Raycast(pos, way, out _hitF, _froDis);
            DebugDraw(pos, way, _froDis, _hitF.collider);
            pos += way.normalized * _froDis;
            // 前方の地面判定がある時
            if (Physics.Raycast(pos, vert, out _hitF, 3))
            {
                f = _speed < 1.0f ? 0.1f : 0;
            }
            DebugDraw(pos, vert, 3, _hitF.collider);
            _speed += f;
            ret += _speed;
            return ret;
        }
        void CheckEnemy()
        {
            Vector3[] vec = new Vector3[]
            {
            new Vector3(-3f,0,-1f),         // 右
            new Vector3(3f,0,-1f),          // 左
            new Vector3(-1f,0,-4f),         // 斜め右
            new Vector3(1f,0,-4f),          // 斜め左
            new Vector3(-1f,0,-9f),         // 直線右
            new Vector3(1f,0,-9f),          // 直線左
            new Vector3(0,0,-1)
            };
            for (int i = 0; i < vec.Length; i++)
            {
                var pos = transform.TransformPoint(_offset);
                var way = transform.TransformDirection(vec[i]);
            }
        }
        // デバック用レイ描画
        public void DebugDraw(Vector3 vec1, Vector3 vec2, float f, Collider col)
        {
            Color color = col ? Color.red : Color.green;
            Debug.DrawRay(vec1, vec2.normalized * f, color, 0, false);
        }
    }
}