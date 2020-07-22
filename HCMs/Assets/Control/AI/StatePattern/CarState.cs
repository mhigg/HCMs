using System.Collections;
using UnityEngine;

public abstract class CarState
{
    public RaycastHit[] _hitW = new RaycastHit[6];
    public RaycastHit _hitF = new RaycastHit();
    public float _speed = 0f;
    public float _brake = 0f;
    public bool _brakeFlag = false;
    // ハンドルの値を決定するための関数
    public abstract float IsHitWay(Vector3 vec1, Vector3 vec2,Vector3 vec3, float dis,int num);
    // 速度を決定するための関数
    public abstract float IsHitFront(Vector3 vec1, Vector3 vec2, Vector3 vec3,float dis);
    // 敵発見時の挙動
    public abstract float HitEnemy(Vector3 vec1, Vector3 vec2, Collider col, float f);
    public void DebugDraw(Vector3 vec1, Vector3 vec2, float f, Collider col)
    {
        Color color = col ? Color.red : Color.green;
        Debug.DrawRay(vec1, vec2.normalized * f, color, 0, false);
    }

    public float GetBrake()
    {
        return _brake;
    }
}