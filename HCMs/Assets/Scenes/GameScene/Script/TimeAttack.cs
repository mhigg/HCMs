using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeAttack : MonoBehaviour
{
    //public FadeManager fadeManager = null;
    public TimeCount timeCounter = null;
    public Text text = null;

    public TimeRanking timeRanking = null;
    public GoalFlag goalFlag = null;

    // Start is called before the first frame update
    void Start()
    {
        timeRanking = timeRanking.GetComponent<TimeRanking>();
        timeRanking.SetUpTimeRanking("TimeAttack", 10, 3);

        goalFlag = goalFlag.GetComponent<GoalFlag>();

        timeCounter = timeCounter.GetComponent<TimeCount>();
        text = text.GetComponent<Text>();
        text.text = "";
    }

    bool isCalledOnce = false;

    bool StartCall  = false;    // StartCountテスト用

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

        // 特定のタイミングでStartCountを呼ぶ
        if(!StartCall)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("レーススタート");
                StartCall = true;
                timeCounter.StartCount();
            }
        }
    }
}
