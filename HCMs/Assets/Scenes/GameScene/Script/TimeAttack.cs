using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class TimeAttack : MonoBehaviour
{
    //public FadeManager fadeManager = null;
    public TimeCount timeCounter = null;

    // Start is called before the first frame update
    void Start()
    {
        timeCounter = timeCounter.GetComponent<TimeCount>();
    }

    bool isCalledOnce = false;

    bool StartCall  = false;     // StartCountテスト用
    bool FinishCall = false;    // FinishCountテスト用

    int rapCnt = 1;
    // Update is called once per frame
    void Update()
    {
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

        // 特定のタイミングでStartCountを呼ぶ
        if(!StartCall)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("レーススタート");
                StartCall = true;
                FinishCall = false;
                timeCounter.StartCount();
            }
        }

        // ゴールしたタイミングでFinishCountを呼ぶ
        if (!FinishCall)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if(rapCnt < 3)
                {
                    Debug.Log("ラップタイム");
                    rapCnt++;
                    timeCounter.RapCount();
                }
                else
                {
                    Debug.Log("ゴール");
                    rapCnt = 1;
                    FinishCall = true;
                    StartCall = false;
                    timeCounter.FinishCount();
                }
            }
        }
    }
}
