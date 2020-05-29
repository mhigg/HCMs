using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;    // このホイールがエンジンにアタッチされているかどうか
    public bool steering; // このホイールがハンドルの角度を反映しているかどうか
    //public bool steering_Back;    // このホイールがエンジンにアタッチされているかどうか
    //public bool motor_Back; // このホイールがハンドルの角度を反映しているかどうか
}
internal enum SpeedType
{
    MPH,
    KPH
}

public class CarControl : MonoBehaviour
{

    public List<AxleInfo> axleInfos;    // 個々の車軸の情報
    public float maxMotorTorque;        // ホイールに適用可能な最大トルク
    public float maxSteeringAngle;      // 適用可能な最大ハンドル角度

    //public float Back_maxMotorTorque;        // ホイールに適用可能な最大トルク
    //public float Back_maxSteeringAngle;      // 適用可能な最大ハンドル角度

    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelBL;
    public WheelCollider wheelBR;

    public Transform wheelFLTrans;
    public Transform wheelFRTrans;
    public Transform wheelBLTrans;
    public Transform wheelBRTrans;
    float steering = 0.0f;
    float motor = 0.0f;
    private Rigidbody m_Rigidbody;
    [SerializeField] private SpeedType m_SpeedType;
    [SerializeField] private float m_Topspeed = 500;    // 速度管理

    public float MaxSpeed { get { return m_Topspeed; } }
    //float steering_Back = 0.0f;
    //float motor_Back = 0.0f;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // wheelcolliderの回転速度に合わせてタイヤモデルを回転させる
        wheelFLTrans.Rotate(wheelFL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        wheelFRTrans.Rotate(wheelFR.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        wheelBLTrans.Rotate(wheelBL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        wheelBRTrans.Rotate(wheelBR.rpm / 60 * 360 * Time.deltaTime, 0, 0);

        // wheelcolliderの角度に合わせてタイヤモデルを回転する（フロントのみ）
        wheelFLTrans.localEulerAngles = new Vector3(wheelFLTrans.localEulerAngles.x, wheelFL.steerAngle - wheelFLTrans.localEulerAngles.z, wheelFLTrans.localEulerAngles.z);
        wheelFRTrans.localEulerAngles = new Vector3(wheelFRTrans.localEulerAngles.x, wheelFR.steerAngle - wheelFRTrans.localEulerAngles.z, wheelFRTrans.localEulerAngles.z);
        wheelBLTrans.localEulerAngles = new Vector3(wheelBLTrans.localEulerAngles.x, wheelBL.steerAngle - wheelBLTrans.localEulerAngles.z, wheelBLTrans.localEulerAngles.z);
        wheelBRTrans.localEulerAngles = new Vector3(wheelBRTrans.localEulerAngles.x, wheelBR.steerAngle - wheelBRTrans.localEulerAngles.z, wheelBRTrans.localEulerAngles.z);

    }

    // 車を移動させる
    public void FixedUpdate()
    {
        motor = -maxMotorTorque * Input.GetAxis("Vertical");
        steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        
        //steering_Back = -Back_maxMotorTorque * Input.GetAxis("Vertical");
        //motor_Back = Back_maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;

                //axleInfo.leftWheel.steerAngle = steering_Back;
                //axleInfo.rightWheel.steerAngle = steering_Back;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;

                //axleInfo.leftWheel.motorTorque = motor_Back;
                //axleInfo.rightWheel.motorTorque = motor_Back;
            }
        }
        CapSpeed();
    }

    private void CapSpeed()
    {
        float speed = m_Rigidbody.velocity.magnitude;
        switch (m_SpeedType)
        {
            case SpeedType.MPH:

                speed *= 2.23693629f;
                if (speed > m_Topspeed)
                    m_Rigidbody.velocity = (m_Topspeed / 2.23693629f) * m_Rigidbody.velocity.normalized;
                break;

            case SpeedType.KPH:
                speed *= 3.6f;
                if (speed > m_Topspeed)
                    m_Rigidbody.velocity = (m_Topspeed / 3.6f) * m_Rigidbody.velocity.normalized;
                break;
        }
    }

}
