using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 各プレイヤーごとにチェックポイントの通過数を加算する
public class CheckPointCount : MonoBehaviour
{

    private int _checkPointCnt;     // チェックポイント通過カウント
    private int _checkPointCntMax;  // チェックポイントの数
    private string _nextCheckPoint;     // 次に通る必要のあるチェックポイント
    private string _lastThroughPoint;   // 最後に通過したチェックポイント
    private string _lastCheckPoint;     // 最後に通過する必要のあるチェックポイント

    void Start()
    {
        _checkPointCnt = 0;
        _nextCheckPoint = "cp1";
        _lastThroughPoint = "";
        CheckPointFlag[] cp = GameObject.Find("CheckPoint").GetComponentsInChildren<CheckPointFlag>();
        _lastCheckPoint = cp[cp.Length - 1].name;

        _checkPointCntMax = GameObject.FindGameObjectsWithTag("CheckPoint").Length;
        Debug.Log("チェックポイント全" + _checkPointCntMax + "個");
        _checkPointCntMax = int.Parse(GameObject.FindGameObjectsWithTag("CheckPoint")[_checkPointCntMax - 1].name.Substring("cp".Length));
        Debug.Log("チェックポイント最終値:" + _checkPointCntMax);
    }

    // ゴールを通過するために必要なチェックポイント通過数をプレイヤーごとにカウントする
    public void CountCheckPoint()
    {
        _checkPointCnt++;
        _nextCheckPoint = $"cp{_checkPointCnt + 1}";
    }

    // ゴールを通過するために必要なチェックポイント通過数をプレイヤーごとにカウントする
    // 別ルート用
    public void CountCheckPoint(string nextCPName)
    {
        _checkPointCnt++;
        _nextCheckPoint = nextCPName;
    }

    public void LastThroughCheckPoint(string lastCPName)
    {
        _lastThroughPoint = lastCPName;
    }

    public string GetNextCPName()
    {
        return _nextCheckPoint;
    }

    // プレイヤーごとに現在通過したチェックポイント数を返す
    public int GetNowThroughCheckPointNum()
    {
        return _checkPointCnt;
    }

    // GoalSpaceを通過可能か判定し結果を返す
    public bool JudgThroughGoalSpace()
    {
        if (_checkPointCnt >= _checkPointCntMax)
        {
            if(_lastThroughPoint == _lastCheckPoint)
            {
                // 通過可能なら通過数を0にする
                _checkPointCnt = 0;
                // 次チェックポイントの設定
                _nextCheckPoint = $"cp{_checkPointCnt + 1}";

                return true;
            }
        }

        return false;
    }
}
