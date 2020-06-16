using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NoGitScene : MonoBehaviour
{
    public TimeCount timeCounter = null;
//    public Text goalText = null;

    public GoalFlag goalFlag = null;

    private const int playerNum = 2;            // プレイヤー人数
    private int _rapMax = 3;
    private int[] rapCnt = new int[playerNum];  // 人数分のラップカウント
    private KeyCode[] playerKeyCode = new KeyCode[playerNum];

    private bool StartCall = false;                     // StartCountテスト用
    private bool[] isFinished = new bool[playerNum];    // FinishCountテスト用

    public TimeRanking timeRanking = null;

    public StartStopController startCounter = null;

    // Start is called before the first frame update
    void Start()
    {
        timeRanking = timeRanking.GetComponent<TimeRanking>();
        timeRanking.SetUpTimeRanking("Battle", playerNum, _rapMax);

        goalFlag = goalFlag.GetComponent<GoalFlag>();
        goalFlag.SetUpGoalFlag(_rapMax);

        timeCounter = timeCounter.GetComponent<TimeCount>();

        startCounter = startCounter.GetComponent<StartStopController>();

        for (int idx = 0; idx < playerNum; idx++)
        {
            rapCnt[idx] = 1;
            isFinished[idx] = false;
        }

        // プレイヤーに応じたキー
        // Debug用
        playerKeyCode[0] = KeyCode.Z;
        playerKeyCode[1] = KeyCode.X;
        playerKeyCode[2] = KeyCode.C;
        playerKeyCode[3] = KeyCode.V;
    }

    bool isCalledOnce = false;

    // Update is called once per frame
    void Update()
    {
        for (int playerID = 0; playerID < playerNum; playerID++)
        {
            if (goalFlag.CheckGoal(playerID))
            {
                Debug.Log("Spaceキーを押してリザルトへ");
                if (!isCalledOnce)
                {
                    ///ここを任意のボタンにしましょう。
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        isCalledOnce = true;
                        FadeManager.Instance.LoadScene("NoGitResult", 2.0f);
                        Debug.Log("Resultへ");
                    }
                }
            }
        }

        // 特定のタイミングでStartCountを呼ぶ
        if (!StartCall)
        {
            if (!(startCounter.startWait))
            {
                Debug.Log("レーススタート");
                StartCall = true;
                timeCounter.StartCount();
            }
        }

        // 順位付け
        /*
         
         
         */

        // ゴールしたタイミングでFinishCountを呼ぶ
        //for (int idx = 0; idx < playerNum; idx++)
        //{
        //    if (!isFinished[idx])
        //    {
        //        if (Input.GetKeyDown(playerKeyCode[idx]))
        //        {
        //            Debug.Log("ラップタイム");
        //            rapCnt[idx]++;    // プレイヤーごとにラップカウントをとる
        //            timeCounter.RapCount(idx);
        //            if (!(rapCnt[idx] <= 3))
        //            {
        //                Debug.Log("ゴール");
        //                isFinished[idx] = true;
        //                timeCounter.FinishCount(idx);
        //            }
        //        }
        //    }
        //}
    }
}
