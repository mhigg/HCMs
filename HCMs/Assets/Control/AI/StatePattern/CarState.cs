public interface CarState
{
    float HandleChangeFromRay();  // ハンドリング
    float BrakeFromRay();                // 減速する
    float AcceleFromRay();               // 加速する
    CarState SerchEnemy();              // 敵がレイにかかった時
}