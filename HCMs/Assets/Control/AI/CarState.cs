// 車の状態によって行う操作を定義するクラス
public interface CarState
{
    CarState Move();        // 移動//
    CarState Straight();    // //直進
    CarState Brake();       // //止まる
    CarState Curve();       // //曲がる
    CarState Back();        // //バック
    CarState Dodge();       // よける
    CarState Hinder();      // 邪魔する/*
    CarState FrontHider();  // /*前に出て邪魔する 
    CarState SideHider();   // /*横から邪魔する
    CarState BackHider();   // /*後ろから邪魔する
   // CarState 
}
