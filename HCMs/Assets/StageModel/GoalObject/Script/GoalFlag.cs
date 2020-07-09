using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalFlag : MonoBehaviour
{
    public Text goalText;               // ゴール時に表示するテキスト
    public TimeCount timeCounter;       // タイムをカウントする
    public ParentCheckPoint parentCp;   // 全チェックポイントの親

    private bool[] _finishCall;         // 終了フラグ
    private int _playerNum;             // プレイヤー人数
    private GameManager _gameMg;        // ゲームマネージャー

    private List<string> _playerName;   // プレイヤー名を保存

    // Start is called before the first frame update
    void Start()
    {
        timeCounter = timeCounter.GetComponent<TimeCount>();
        parentCp = parentCp.GetComponent<ParentCheckPoint>();

        GameObject[] racingCars = GameObject.FindGameObjectsWithTag("RacingCar");
        _playerNum = racingCars.Length;
        _finishCall = new bool[_playerNum];
        for (int playerID = 0; playerID < _playerNum; playerID++)
        {
            _finishCall[playerID] = false;
        }

        _playerName = new List<string>();
        for(int idx = 0; idx < racingCars.Length; idx++)
        {
            // 全プレイヤー分のプレイヤー名を保存
            _playerName.Add(racingCars[idx].transform.parent.name);
            Debug.Log(_playerName[idx]);
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
        GameObject throughObject = other.gameObject;

        int playerID = 0;
        for (int idx = 0; idx < _playerNum; idx++)
        {
            if (throughObject.transform.parent.name == _playerName[idx])
            {
                // プレイヤー名を照合して、playerIDに変換
                playerID = idx;
                Debug.Log("プレイヤー" + playerID + "ゴール通過");
            }
            else
            {
                Debug.LogError("プレイヤー名が未登録です");
            }
        }

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
