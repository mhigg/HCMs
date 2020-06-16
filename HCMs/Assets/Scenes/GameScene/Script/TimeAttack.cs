using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeAttack : MonoBehaviour
{
    //public FadeManager fadeManager = null;
    public TimeCount timeCounter = null;

    public TimeRanking timeRanking = null;
    public GoalFlag goalFlag = null;

    public StartStopController startCounter = null;

    private int _rapMax = 3;

    // Start is called before the first frame update
    void Start()
    {
        timeRanking = timeRanking.GetComponent<TimeRanking>();
        // 第一引数をコース名にする
        timeRanking.SetUpTimeRanking("Course2", 11, _rapMax);

        goalFlag = goalFlag.GetComponent<GoalFlag>();
        goalFlag.SetUpGoalFlag(_rapMax);

        timeCounter = timeCounter.GetComponent<TimeCount>();

        startCounter = startCounter.GetComponent<StartStopController>();
    }

    private bool isCalledOnce = false;

    private bool StartCall = false;    // StartCountテスト用

    // Update is called once per frame
    void Update()
    {
        int playerID = 0;   // Debug用 1プレイヤー目の0
        if (goalFlag.CheckGoal(playerID))
        {
            Debug.Log("Spaceキーを押してリザルトへ");
            if (!isCalledOnce)
            {
                ///ここを任意のボタンにしましょう。
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isCalledOnce = true;
                    FadeManager.Instance.LoadScene("TimeAttackResult", 2.0f);
                    Debug.Log("Resultへ");
                }
            }
        }

        // Debug用　横転等で動けなくなったとき用
        if (!isCalledOnce)
        {
            ///ここを任意のボタンにしましょう。
            if (Input.GetKeyDown(KeyCode.Return))
            {
                isCalledOnce = true;
                FadeManager.Instance.LoadScene("TimeAttack02", 2.0f);
                Debug.Log("再スタート");
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
    }
}
