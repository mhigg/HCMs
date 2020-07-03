using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CpuController : MonoBehaviour
    {
        private CarController _carCtl; // 車の操作

        Vector3 _dir;

        private void Awake()
        {
            // get the car controller
            _carCtl = GetComponent<CarController>();
        }

        private void FixedUpdate()
        {
            //Physics.Raycast(this.transform.position,);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = -0.2f;
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            _carCtl.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }
    }
}
