
public class IdleState :  CarState
{
    float _horizon = 0f;            // 横ハンドリング
    float _vertical = 0f;           // 前後ハンドリング

    void Update()
    {
        _vertical = -0.3f;
        _horizon = 0;
        _vertical = 0;
    }

    public float HandleChangeFromRay()
    {
        _horizon += 1;
        return _horizon;
    }
    public float BrakeFromRay()
    {
        return 0;
    }

    public float AcceleFromRay()
    {
        return 0;
    }

    public CarState SerchEnemy()
    {
        return this;
    }
}
