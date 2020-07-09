public interface CarState
{
    CarState HitRaycast();      // レイが道なりにある時
    CarState ExitRaycast();     // レイが道から外れた時   
    CarState SerchEnemy();      // 敵がレイにかかった時
}