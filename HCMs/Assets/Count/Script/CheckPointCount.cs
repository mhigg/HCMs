using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointCount : MonoBehaviour
{
    private int _checkPointCnt;     // チェックポイント通過カウント
    private int _checkPointCntMax;  // チェックポイントの数

    void Start()
    {
        _checkPointCnt = 0;

        // ゴールによってカウントするチェックポイントのタグを変える
        _checkPointCntMax = GameObject.FindGameObjectsWithTag("CheckPoint").Length;
        Debug.Log("チェックポイント全" + _checkPointCntMax + "個");
    }

    // ゴールを通過するために必要なチェックポイント通過数をプレイヤーごとにカウントする
    public void CountCheckPoint()
    {
        _checkPointCnt++;
    }

    // プレイヤーごとに現在通過したチェックポイント数を返す
    public int GetNowThroughCheckPointNum()
    {
        return _checkPointCnt;
    }

    // GoalSpaceを通過可能か判定し結果を返す
    public bool JudgThroughGoalSpace()
    {
        bool retFlag = (_checkPointCnt == _checkPointCntMax);
        if (retFlag)
        {
            // 通過可能なら通過数を0にする
            _checkPointCnt = 0;
        }

        return retFlag;
    }
}
