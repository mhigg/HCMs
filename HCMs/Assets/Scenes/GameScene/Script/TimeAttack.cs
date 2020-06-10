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

    // Start is called before the first frame update
    void Start()
    {
        timeRanking = timeRanking.GetComponent<TimeRanking>();
        timeRanking.SetUpTimeRanking("TimeAttack", 11, 3);

        goalFlag = goalFlag.GetComponent<GoalFlag>();

        timeCounter = timeCounter.GetComponent<TimeCount>();

        startCounter = startCounter.GetComponent<StartStopController>();
    }

    private bool isCalledOnce = false;

    private bool StartCall  = false;    // StartCountテスト用

    // Update is called once per frame
    void Update()
    {
        if (goalFlag.CheckGoal())
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
                FadeManager.Instance.LoadScene("TimeAttackScene", 2.0f);
                Debug.Log("再スタート");
            }
        }

        // 特定のタイミングでStartCountを呼ぶ
        if (!StartCall)
        {
            if(!(startCounter.startWait))
            {
                Debug.Log("レーススタート");
                StartCall = true;
                timeCounter.StartCount();
            }
        }
    }
}
