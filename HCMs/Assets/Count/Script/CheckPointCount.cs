using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointCount : MonoBehaviour
{
    private int[] checkPointCnt;        // チェックポイント通過カウント
    private int checkPointCntMax;       // チェックポイントの数
    private int _playerNum;             // プレイヤー数

    void Start()
    {
        _playerNum = GameObject.FindGameObjectsWithTag("RacingCar").Length;

        checkPointCnt = new int[_playerNum];
        for (int playerID = 0; playerID < _playerNum; playerID++)
        {
            checkPointCnt[playerID] = 0;
        }

        // ゴールによってカウントするチェックポイントのタグを変える
        checkPointCntMax = GameObject.FindGameObjectsWithTag("CheckPoint").Length;
        Debug.Log("チェックポイント全" + checkPointCntMax + "個");
    }

    // ゴールを通過するために必要なチェックポイント通過数をプレイヤーごとにカウントする
    public void CountCheckPoint(int playerID)
    {
        checkPointCnt[playerID]++;
    }

    // プレイヤーごとに現在通過したチェックポイント数を返す
    public int GetNowThroughCheckPointNum(int playerID)
    {
        return checkPointCnt[playerID];
    }

    // GoalSpaceを通過可能か判定し結果を返す
    public bool JudgThroughGoalSpace(int playerID)
    {
        bool retFlag = (checkPointCnt[playerID] == checkPointCntMax);
        if (retFlag)
        {
            // 通過可能なら通過数を0にする
            checkPointCnt[playerID] = 0;
        }

        return retFlag;
    }
}
