public interface CarState
{
    CarState HitrayCast();      // レイが道なりにある時
    CarState ExitRayCast();     // レイが道から外れた時   
    CarState SerchEnemy();      // 敵がレイにかかった時
}