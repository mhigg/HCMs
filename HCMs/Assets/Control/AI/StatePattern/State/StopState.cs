using UnityEngine;
using System.Collections;

public class StopState : CarState
{
    public override float IsHitWay(Vector3 vec1, Vector3 vec2, Vector3 vec3, float dis, int num)
    {
        var pos = vec1;
        var dir = vec2;
        float move = 0;
        if (Physics.Raycast(pos, dir, out _hitW[num], dis))
        {
            move += num * 2 - 1;
        }
        DebugDraw(pos, dir, dis, _hitW[num].collider);
        return move;
    }

    public override float IsHitFront(Vector3 vec1, Vector3 vec2, Vector3 vec3, float dis)
    {

        return 0;
    }

    public override float HitEnemy(Vector3 vec1, Vector3 vec2, Collider col, float f)
    {
        return 0;
    }
}
