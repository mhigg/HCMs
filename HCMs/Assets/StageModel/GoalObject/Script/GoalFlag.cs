using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalFlag : MonoBehaviour
{
    public Text goalText;               // ゴール時に表示するテキスト
    public TimeCount timeCounter;       // タイムをカウントする
    public ParentCheckPoint parentCp;   // 全チェックポイントの親

    private bool[] _finishCall;
    private int _playerNum;             // プレイヤー人数
    private GameManager _gameMg;        // ゲームマネージャー

    // Start is called before the first frame update
    void Start()
    {
        timeCounter = timeCounter.GetComponent<TimeCount>();
        parentCp = parentCp.GetComponent<ParentCheckPoint>();

        GameObject[] racingCars = GameObject.FindGameObjectsWithTag("RacingCar");
        _playerNum = racingCars.Length;

        _finishCall = new bool[_playerNum];
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
        GameObject throughObject = other.gameObject;

        int playerID = FindInfoByScene.Instance.GetPlayerID(throughObject.transform.parent.name);
        Debug.Log("プレイヤー" + playerID + "ゴール通過");

        if (!_finishCall[playerID])
        {
            if (throughObject.tag == "RacingCar")
            {
                if (throughObject.GetComponent<CheckPointCount>().JudgThroughGoalSpace())
                {
                    // 全チェックポイントを未通過状態にする
                    parentCp.ResetCheckPointIsThrough(playerID);
                    // 周回数とラップタイムをカウントする
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
