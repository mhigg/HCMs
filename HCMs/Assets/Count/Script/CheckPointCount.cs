using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointCount : MonoBehaviour
{
    private int[] _checkPointCnt;       // チェックポイント通過カウント
    private int _checkPointCntMax;      // チェックポイントの数

    void Start()
    {
        int playerNum = GameObject.FindGameObjectsWithTag("RacingCar").Length;

        _checkPointCnt = new int[playerNum];
        for (int playerID = 0; playerID < playerNum; playerID++)
        {
            _checkPointCnt[playerID] = 0;
        }

        // ゴールによってカウントするチェックポイントのタグを変える
        _checkPointCntMax = GameObject.FindGameObjectsWithTag("CheckPoint").Length;
        Debug.Log("チェックポイント全" + _checkPointCntMax + "個");
    }

    // ゴールを通過するために必要なチェックポイント通過数をプレイヤーごとにカウントする
    public void CountCheckPoint(int playerID)
    {
        _checkPointCnt[playerID]++;
    }

    // プレイヤーごとに現在通過したチェックポイント数を返す
    public int GetNowThroughCheckPointNum(int playerID)
    {
        return _checkPointCnt[playerID];
    }

    // GoalSpaceを通過可能か判定し結果を返す
    public bool JudgThroughGoalSpace(int playerID)
    {
        bool retFlag = (_checkPointCnt[playerID] == _checkPointCntMax);
        if (retFlag)
        {
            // 通過可能なら通過数を0にする
            _checkPointCnt[playerID] = 0;
        }

        return retFlag;
    }
}
