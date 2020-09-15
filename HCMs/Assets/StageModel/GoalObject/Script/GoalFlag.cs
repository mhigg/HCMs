using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;

public class GoalFlag : MonoBehaviour
{
    public Image finishImage;           // ゴール時に表示するテキスト
    public TimeCount timeCounter;       // タイムをカウントする
    public ParentCheckPoint parentCp;   // 全チェックポイントの親
    public GameObject car1;
    public GameObject car2;

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
            if (_finishCall[playerID])
            {
                // ゴールしたプレイヤーを自動操縦に切り替える
                GameObject finishPlayer = GameObject.Find(FindInfoByScene.Instance.GetPlayerName(playerID));
                finishPlayer.GetComponent<CarUserControl>().enabled = false;
                finishPlayer.GetComponent<AutoRun>().enabled = true;

                //if (playerID == 0)
                //{
                //    car1.GetComponent<CarUserControl>().enabled = false;
                //    car1.GetComponent<AutoRun>().enabled = true;
                //}
                //if (playerID == 1)
                //{
                //    car2.GetComponent<CarUserControl>().enabled = false;
                //    car2.GetComponent<AutoRun>().enabled = true;
                //}
            }
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
                    throughObject.GetComponent<LapCount>().CountLap();
                    timeCounter.LapCount(playerID);
                }

                if (throughObject.GetComponent<LapCount>().CheckLapCount())
                {
                    Debug.Log("プレイヤー" + playerID + "ゴール");

                    // 相手がCPUの時は、1Pゴール時のみFINISHを表示
                    Transform canvasTF = GameObject.FindGameObjectWithTag("CounterCanvas").transform;

                    if(canvasTF.parent.name == "DualScreenCanvas")
                    {
                        // 画面分割時はそれぞれの画面にFINISH表示
                        Vector3 imagePos = new Vector3((_playerNum - 1) * 500 * (playerID * 2 - 1) + Screen.width / 2, Screen.height / 2, 0);
                        Image _finishImage = Instantiate(finishImage, imagePos, Quaternion.identity);
                        _finishImage.transform.SetParent(canvasTF);
                    }
                    else if(throughObject.transform.parent.tag != "CPU")
                    {
                        // 分割してない時でゴールしたのがCPUじゃなければ画面の真ん中にFINISH表示
                        Vector3 imagePos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
                        Image _finishImage = Instantiate(finishImage, imagePos, Quaternion.identity);
                        _finishImage.transform.SetParent(canvasTF);
                    }
                    else
                    {
                        // それ以外は表示処理なし
                    }

                    timeCounter.FinishCount(playerID);
                    _finishCall[playerID] = true;
                }

                throughObject.GetComponent<CheckPointCount>().LastThroughCheckPoint(this.name);
            }
        }
    }
}
