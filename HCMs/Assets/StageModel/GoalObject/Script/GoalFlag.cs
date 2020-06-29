using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalFlag : MonoBehaviour
{
    public Text goalText;           // ゴール時に表示するテキスト
    public TimeCount timeCounter;   // タイムをカウントする

    private bool[] _finishCall;     // 終了フラグ
    private int _playerNum;         // プレイヤー人数

    // Start is called before the first frame update
    void Start()
    {
        timeCounter = timeCounter.GetComponent<TimeCount>();

        _playerNum = GameObject.FindGameObjectsWithTag("RacingCar").Length;
        _finishCall = new bool[_playerNum];
        for (int playerID = 0; playerID < _playerNum; playerID++)
        {
            _finishCall[playerID] = false;
        }
    }

    // ゴール可能かどうかの判定を返す
    public bool CheckFinish()
    {
        bool retFinish = true;
        for(int playerID = 0; playerID < _playerNum; playerID++)
        {
            // 一つでもfalseがあるとfalseになり、まだゴールしていないプレイヤーがいると判断
            retFinish &= _finishCall[playerID];
        }
        return retFinish;
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
         other.gameObjectのプレイヤー名を照会してその添字をplayerIDとする
         ex)
         for(int idx = 0; idx < playerName.Length; idx++)
         {
            if(other.gameObject.name == playerName[idx])
            {
                playerID = idx;
            }
         }
         つまり0～3となる
         現状は0(1プレイヤー目)とする
         */

        GameObject throughObject = other.gameObject;

        // 現状プレイヤー名の登録は実装していないため、車のbodyの名前を0と1にして直接playerIDとして扱う
        int playerID = int.Parse(throughObject.name);    // ※PLAYERNAME※
        Debug.Log("プレイヤー" + playerID + "ゴール通過");

        if (!_finishCall[playerID])
        {
            if (throughObject.tag == "RacingCar")
            {
                if (throughObject.GetComponent<CheckPointCount>().JudgThroughGoalSpace())
                {
                    throughObject.GetComponent<RapCount>().CountRap();
                    timeCounter.RapCount(playerID);
                }

                if (throughObject.GetComponent<RapCount>().CheckRapCount())
                {
                    Debug.Log("プレイヤー" + playerID + "ゴール");
                    Vector3 pos = new Vector3((_playerNum - 1) * 500 * (playerID * 2 - 1) + 960, 540, 0);
                    Text _goalText = Instantiate(goalText, pos, Quaternion.identity);
                    _goalText.text = "ＦＩＮＩＳＨ！";
                    _goalText.transform.SetParent(GameObject.Find("CounterCanvas").transform);
                    timeCounter.FinishCount(playerID);
                    _finishCall[playerID] = true;
                }
            }
        }
    }
}
