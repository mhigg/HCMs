using UnityEngine;

public class IdleState : CarState
{
    float[] _turnVol = { 0.1f, 0.2f, 0.5f };
    public override float IsHitWay(Vector3 vec1, Vector3 vec2, Vector3 vec3, float dis, int num)
    {
        var pos = vec1;
        float move = 0;
        Physics.Raycast(pos, vec2, out _hitW[num], dis);
        DebugDraw(pos, vec2, dis, _hitW[num].collider);
        pos += vec2.normalized * dis;
        if (Physics.Raycast(pos, vec3, out _hitW[num], 3))
        {
            move = (num % 2 * 2 - 1f) * -1f * _turnVol[num/2];
        }
        DebugDraw(pos, vec3, 3, _hitW[num].collider);
        return move;
    }

    public override float IsHitFront(Vector3 vec1, Vector3 vec2, Vector3 vec3, float dis)
    {
        float f = 0;
        var pos = vec1;
        if (Physics.Raycast(pos, vec2, out _hitF, dis))
        {
        }
        DebugDraw(pos, vec2, dis, _hitF.collider);
        pos += vec2.normalized * dis;
        if (Physics.Raycast(pos, vec3, out _hitF, 3))
        {
            f = _speed > -1f ? -0.1f : 0;
            _brake += _brake > 0 ? -0.1f : 0f; 
        }
        else
        {
            _brake = 1;
            _speed /= 2;
        }
        DebugDraw(pos, vec3, 3, _hitF.collider);
        _speed += f;
        return _speed;
    }

    public override float HitEnemy(Vector3 vec1, Vector3 vec2, Collider col, float f)
    {
        return 0;
    }
}
