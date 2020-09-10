using System.Collections;
using UnityEngine;

public abstract class CarState
{
    public RaycastHit[] _hitW = new RaycastHit[6];
    public RaycastHit _hitF = new RaycastHit();
    private RaycastHit[] _hitEnemy = new RaycastHit[7];
    public float _speed = 0f;
    public float _brake = 0f;
    public bool _brakeFlag = false;
    public CarState _nextState = null;
    public float _enemyDis = 10000;
    // ハンドルの値を決定するための関数
    public abstract float HandleDir(Vector3 pos, Vector3 way,Vector3 vert, float dis,int num);
    // 速度を決定するための関数
    public abstract float AcceleStep(Vector3 vec1, Vector3 vec2, Vector3 vec3,float dis);
    // 敵の検知
    // public abstract CarState ChangeState(Vector3 pos, Vector3 way, float dis, int num);
    public void SerchEnemy(Vector3 pos, Vector3 way, float dis, int num)
    {

    }
    // デバック用レイ描画
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