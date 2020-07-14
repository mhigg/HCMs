// CPUの情報を統括するクラス
using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarGeneral : MonoBehaviour
    {
        private RaycastHit[] _hit = new RaycastHit[2];
        private CarState _state = null;     // 車の状態
        private CarController _carCtl;      // 車の操作
        private Vector3 _offset = new Vector3(0,0.2f,-4.5f);
        float _dis = 10.0f;

        Vector3[] _dir = new Vector3[]      // レイの方向
        { 
            new Vector3(-1f,-0.25f,-3f),        // 右
            new Vector3(1f,-0.25f,-3f)          // 左
        };

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
            for (int i = 0; i < _dir.Length; i++)
            {
                var pos = transform.TransformPoint(_offset);
                var dir = transform.TransformDirection(_dir[i]);
                // レイキャストで判定
                // 判定あればHitRayCast
                if(Physics.Raycast(pos, dir, out _hit[i], _dis))
                {
                    this._state.HitRaycastWay();
                }


                DebugDraw(pos, dir, _dis, _hit[i].collider);
            }

            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            _carCtl.Move(h, v, v, handbrake);
        }

        CarState GetState()
        {
            return this._state;
        }

        public CarState HitRaycastEvent()
        {
            return _state.HitRaycastCenter();
        }
        public CarState ExitRaycast()
        {
            return _state.ExitRaycast();
        }
        public CarState SerchEnemyEvent()
        {
            return _state.SerchEnemy();
        }

        void DebugDraw(Vector3 vec1,Vector3 vec2,float f,Collider col)
        {
            Color color = col ? Color.red : Color.green;
            Debug.DrawRay(vec1, vec2.normalized * f, color, 0,false);
        }
    }
}
