// CPUの情報を統括するクラス
using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarGeneral : MonoBehaviour
    {
        private CarState _state = null;     // 車の状態
        private CarController _carCtl;      // 車の操作

        private RaycastHit[] _hit = new RaycastHit[2];
        private Vector3 _offset = new Vector3(0,1.5f,-4.5f);
        float _wayDis = 15.0f;                      // 左右レイの長さ
        float froDis = 20f;                         // 直線レイの長さ
        float _vertDis = 3.0f;                      // 垂直レイの長さ
        Vector3[] _wayDir = new Vector3[]           // 左右のレイの方向
        { 
            new Vector3(-2f,0,-1.5f),        // 右
            new Vector3(2f,0,-1.5f)          // 左
        };
        Vector3 _froDir = new Vector3(0,0,-10);     // まっすぐのレイ
        Vector3 _vertDir = new Vector3(0, -2f, 0);  // 垂直のレイ
        

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

        // Moveとステートの変数とそれに渡す値の設定
        void FixedUpdate()
        {
            float v = CheckFront();
            float h = CheckWay();
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            _carCtl.Move(h, v, v, handbrake);
        }
        float CheckWay()
        {
            float f = 0;
            // 左右レイの判定
            for (int i = 0; i < _wayDir.Length; i++)
            {
                var pos = transform.TransformPoint(_offset);
                var dir = transform.TransformDirection(_wayDir[i]);
                // 左右レイ
                Physics.Raycast(pos, dir, out _hit[i], _wayDis);
                // そこから下に引くレイ
                pos += dir.normalized * _wayDis;
                var ray = transform.TransformDirection(_vertDir);
                if (Physics.Raycast(pos, ray, out _hit[i], 3))
                {
                    f += _state.IsHitWay(pos,dir,_wayDis,i);
                }
            }
            return f;
        }
        float CheckFront()
        {
            return -1;
        }
        float CheckEnemy()
        {
            return 0;
        }
        
    }
}
