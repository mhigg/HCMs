using UnityEngine;
using System;

// 追従するステート
public class FollowState : CarState
{
    public override float HandleDir(Vector3 pos, Vector3 way, Vector3 vert, float dis, int num)
    {
        float move = 0;
        DebugDraw(pos, way, dis, _hitW[num].collider);
        // 車から目標までのベクトル
        var targetVec = way - pos;
        //車の向き
        var carVec = new Vector3(0, 0, 1);
        //内積をとり、逆走していないか判定する
        // 逆走していれば、何らかの処理をする
        if (Vector3.Dot(targetVec, carVec) <= 0)
        {
            // 何らかの処理
            return -1f;
        }
        // 外積を取りハンドルを切る方向を判定する
        //Vector3.Cross()
        return move;
    }

    public override float AcceleStep(Vector3 pos, Vector3 way, Vector3 vert, float dis)
    {
        _brakeFlag = false;
        float f = 0;
        Physics.Raycast(pos, way, out _hitF, dis);
        DebugDraw(pos, way, dis, _hitF.collider);
        pos += way.normalized * dis;
        // 前方の地面判定がある時
        if (Physics.Raycast(pos, vert, out _hitF, 3))
        {
            f = _speed < 1.0f ? 0.1f : 0;
            _brake += _brake > 0 ? -0.1f : 0f;
        }
        // ないとき
        else
        {
            _brake = 1f;
            _speed -= 0.03f;
            _brakeFlag = true;
        }
        DebugDraw(pos, vert, 3, _hitF.collider);
        _speed += f;
        return _speed;
    }
}
