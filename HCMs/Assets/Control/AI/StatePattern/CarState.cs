using System.Collections;
using UnityEngine;

public abstract class CarState
{
    public RaycastHit[] _hit = new RaycastHit[2];
    // ハンドルの値を決定するための関数
    public abstract float IsHitWay(Vector3 vec1, Vector3 vec2, float dis,int num);
    // 速度を決定するための関数
    public abstract float HitFront(Vector3 vec1, Vector3 vec2, Collider col, float f);
    // 敵発見時の挙動
    public abstract float HitEnemy(Vector3 vec1, Vector3 vec2, Collider col, float f);
    public void DebugDraw(Vector3 vec1, Vector3 vec2, float f, Collider col)
    {
        Color color = col ? Color.red : Color.green;
        Debug.DrawRay(vec1, vec2.normalized * f, color, 0, false);
    }
}