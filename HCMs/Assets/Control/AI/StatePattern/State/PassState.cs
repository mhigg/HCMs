public class PassState : CarState
{
    public float HandleChangeFromRay(float f)
    {
        return 0f;
    }
    public float BrakeFromRay()
    {
        return 0f;
    }
    public float AcceleFromRay()
    {
        return 0f;
    }

    public CarState SerchEnemy()
    {
        return this;
    }
}
