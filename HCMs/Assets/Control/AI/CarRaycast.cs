using System.Collections.Generic;
using UnityEngine;
enum RayNum
    {
        Front,
        FRight,
        FLeft,
        //Back,
        //BRight,
        //BLeft,
        Max
    }
public class CarRaycast : MonoBehaviour
{
    // レイの本数
    private int _rayNum = (int)RayNum.Max;  
    // レイの長さ
    private float _distance = 3.0f;

    // レイの方向
    private Vector3[] _dir = new Vector3[]
    {
        new Vector3(0.0f,-2f,-2.0f),
        new Vector3(2.0f,-2f,-2.0f),
        new Vector3(-2.0f,-2f,-2.0f)
    };

    // 前、右前、左前
    public RaycastHit[] _hitRay 
        = new RaycastHit[(int)RayNum.Max];

    // worldの座標
    private Vector3 _position;
    // worldの方向
    private Vector3 _direction;

    // 前方レイの発射位置までのオフセット
    private Vector3 _frontOffset = new Vector3(0.0f,1.0f,-3.0f);

    public bool IsHitFront { get; set; }    // 前方の当たり判定
    public bool IsHitRound { get; set; }    // 周囲の当たり判定

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0;i < _rayNum;i++)
        {
            _position = transform.TransformPoint(_frontOffset);
            _direction = transform.TransformDirection(_dir[i]);
            // 前方の判定
            IsHitFront = Physics.Raycast(_position, _direction, out _hitRay[i], _distance);
            RayDraw(_position, _direction, _distance, _hitRay[i].collider);
        }
    }

    void RayDraw(Vector3 position,Vector3 direction,float distance,Collider collider)
    {
        Color color = collider ? Color.red : Color.green;
        Debug.DrawRay(position, direction.normalized * distance, color, 0, false);
        Debug.Log("うんち！！！！！！！！！");
    }
}
