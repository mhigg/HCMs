// CPUの情報を統括するクラス
public class CarGeneral
{
    // 車の状態
    private CarState _state = null;
    public CarGeneral()
    {
        if(_state == null)
        {
            _state = null; 
        }
    }

    public void Update()
    {

    }

    public CarState HitRaycastEvent()
    {
        return _state.HitRaycast();
    }
    public CarState ExitRaycast()
    {
        return _state.ExitRaycast();
    }
    public CarState SerchEnemyEvent()
    {
        return _state.SerchEnemy();
    }
}
