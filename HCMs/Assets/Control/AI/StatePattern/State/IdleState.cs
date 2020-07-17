public class IdleState :  CarState
{
    void Update()
    {
    }

    public float HandleChangeFromRay(float f)
    {
        float ret = f;
        return ret;
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
