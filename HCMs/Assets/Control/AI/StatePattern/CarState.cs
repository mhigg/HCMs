public interface CarState
{
    CarState HitRaycastCenter();    // 中央レイ判定時
    CarState HitRaycastWay();       // 分岐レイ判定時
    CarState ExitRaycast();         // すべてのレイが道から外れた時   
    CarState SerchEnemy();          // 敵がレイにかかった時
}