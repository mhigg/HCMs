// CPUの情報を統括するクラス
using UnityEngine;
using System.Collections.Generic;
using System;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarGeneralPoint : MonoBehaviour
    {
        private CarState _state = null;     // 車の状態
        private CarController _carCtl;      // 車の操作

        // 目標地点を決めるための変数
        public GameObject _target;          // ターゲットを持つオブジェクト
        private List<Vector3> _points;      // 目標地点の座標
        private Vector3 _plateSize;         // ターゲットのサイズ
        private Vector3 _plateOffset;       // ターゲットのオフセット
        private int _pointNum = 0;          // 現在のポイントの番号

        // レイキャストに使う変数
        private Vector3 _offset = 
            new Vector3(0, 1.5f, 4.5f);     // レイのオフセット
        private float _froDis = 45f;        // 直線レイの長さ
        private Vector3 _froDir = 
            new Vector3(0, 0, 1);           // 直線レイ   
        private float[] _wayDis = 
            { 5.0f, 30f, 45f };             // 左右レイの長さ                               
        private Vector3[] _wayDir =         // 左右レイの方向 
            new Vector3[]                  
        {
            new Vector3(-3f,0,1f),          // 右
            new Vector3(3f,0,1f),           // 左
            new Vector3(-1f,0,4f),          // 斜め右
            new Vector3(1f,0,4f),           // 斜め左
            new Vector3(-1f,0,9f),          // 直線右
            new Vector3(1f,0,9f)            // 直線左
        };
        private Vector3 _vertDir = 
            new Vector3(0, -2f, 0);         // 垂直レイ
        private float[] _turnVol = 
            { 0.4f, 0.35f, 0.15f };         // ウェイによって曲がる量

        private void Awake()
        {
            _carCtl = GetComponent<CarController>();
            SetRandomPoints();
        }
        public CarGeneralPoint()
        {
            if (_state == null)
            {
                _state = new FollowState();
            }
        }
        void FixedUpdate()
        {
            var child = _target.transform;
            var cnt = child.childCount;
            for (int i = 0; i < cnt; i++)
            {
                DrawRayRoot(_points[i], _points[(i + 1) % cnt]);
            }
            CheckEnemy();
            float v = CheckFront();
            float h = CheckWay();
            float b = 0;// _state.GetBrake();
            var sp = _carCtl.CurrentSpeed;
            if (sp >= 40 && sp <= 60)
            {
                _carCtl.m_GearUpPush = true;
            }
            else if (sp >= 60)
            {
                _carCtl.m_GearUpPush = false;
            }
            _carCtl.Move(h, v, v, b);
        }

        float CheckPoints()
        {
            float f = 0.0f;
            return f;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "target")
            {
                _pointNum++;
            }
        }

        // 進行方向と目標地点との内・外積を求めて
        // ハンドルの量を決める
        float CheckWay()
        {
            float handle = 0;
            //車の座標(始点)
            var pos = transform.TransformPoint(_offset);
            // 目標の座標(終点)
            var way = _points[_pointNum] - pos;
            var vert = transform.TransformDirection(_vertDir);
            // 目標までの距離
            var dis = (float)Math.Sqrt(way.x * way.x + way.z * way.z);
            handle += _state.HandleDir(transform.position, way, transform.TransformDirection(_froDir), dis, 0);// * _turnVol[i / 2];
            return handle;
        }
        float CheckFront()
        {
            float f = 0;
            var pos = transform.TransformPoint(_offset);
            var way = transform.TransformDirection(_froDir);
            var vert = transform.TransformDirection(_vertDir);
            f += _state.AcceleStep(pos, way, vert, _froDis);
            return f;
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
                //_state.IsHitEnemy(pos, way, _wayDis[i / 2], i);
            }
        }
        CarState ChangeState()
        {
            return _state;
        }

        // ランダムにポイントを決める
        void SetRandomPoints()
        {
            _points = new List<Vector3>();
            for (var i = 0; i < _target.transform.childCount; i++)
            {
                var tra = _target.transform.GetChild(i).transform;
                _plateSize = tra.localScale;
                _plateOffset = tra.localPosition;
                // 座標を決める
                var posx = GetRandomVec() * _plateSize.x;
                var posy = 5;// GetRandomVec() * _plateSize.y;
                var posz = GetRandomVec() * _plateSize.z;
                var pos = new Vector3(tra.position.x, 0, tra.position.z);
                var position = new Vector3(posx, posy, posz) + pos;
                //position = pos - position;
                _points.Add(position);
            }
        }
        float GetRandomVec()
        {
            float range = UnityEngine.Random.Range(-0.5f, 0.5f);
            return range;
        }

        void DrawRayRoot(Vector3 vec1,Vector3 vec2)
        {
            Color color = Color.yellow;
            var dir = vec2 - vec1;
            Debug.DrawRay(vec1, dir, color, 0, false);
        }
    }
}
