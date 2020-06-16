using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    public StartStopController startCounter;
    public TimeCount timeCounter;

    public TimeRanking timeRanking;
    public GoalFlag goalFlag;

    private int _rapMax = 3;

    // Start is called before the first frame update
    void Start()
    {
        startCounter = startCounter.GetComponent<StartStopController>();
        timeCounter = timeCounter.GetComponent<TimeCount>();
        timeRanking = timeRanking.GetComponent<TimeRanking>();
        timeRanking.SetUpTimeRanking("battle01", 2, _rapMax);
        goalFlag = goalFlag.GetComponent<GoalFlag>();
        goalFlag.SetUpGoalFlag(_rapMax);
    }

    bool isCalledOnce = false;
    bool StartCall = false;

    // Update is called once per frame
    void Update()
    {
        if (!isCalledOnce)
        {
            ///ここを任意のボタンにしましょう。
            if (Input.GetKeyDown("space"))
            {
                isCalledOnce = true;
                FadeManager.Instance.LoadScene("BattleResult", 2.0f);
                Debug.Log("Resultへ");
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
