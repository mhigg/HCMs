// CPUの情報を統括するクラス
using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarGeneral : MonoBehaviour
    {
        private CarState _state = null; // 車の状態
        private CarController _carCtl;  // 車の操作


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

        public void Update()
        {
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = -0.5f;
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            _carCtl.Move(h, v, v, handbrake);
        }

        CarState GetState()
        {
            return this._state;
        }

        public CarState HitRaycastEvent()
        {
            return _state.HitRaycast();
        }
        public CarState ExitRaycast()
        {
            return _state.ExitRaycast();
        }
        public CarState SerchEnemyEvent()
        {
            return _state.SerchEnemy();
        }
    }
}
