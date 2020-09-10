using UnityEngine;
//普通の挙動をするステート 
public class IdleState : CarState
{    
    public override float HandleDir(Vector3 pos, Vector3 way, Vector3 vert, float dis, int num)
    {
        float move = 0;
        Physics.Raycast(pos, way, out _hitW[num], dis);
        DebugDraw(pos, way, dis, null);
        pos += way.normalized * dis;
        if (Physics.Raycast(pos, vert, out _hitW[num], 3))
        {
            move = num % 2 * 2 - 1f;
        }
        //意味ない
        if (_brakeFlag)
        {
            move *= 2;
        }
        DebugDraw(pos, vert, 3, _hitW[num].collider);
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
    //public override CarState IsHitEnemy(Vector3 pos, Vector3 way, float dis, int num)
    //{
    //    if(Physics.Raycast(pos, way, out _hitEnemy[num], dis))
    //    {
    //        _nextState = new FollowState();
    //    }
    //    DebugDraw(pos, way, dis, _hitEnemy[num].collider);
    //    return _nextState;
    //}
}
