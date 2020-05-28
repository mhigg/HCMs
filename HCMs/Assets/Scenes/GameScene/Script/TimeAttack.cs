﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeAttack : MonoBehaviour
{
    //public FadeManager fadeManager = null;
    public TimeCount timeCounter = null;
    public Text text = null;

    string playerID = "P1";     // プレイヤーごとに持っていて、プレイヤーから渡されるのが理想
    const int playerNum = 1;    // プレイ人数
    int[] rapCnt = new int[playerNum];  // 人数分のラップカウント

    public TimeRanking timeRanking = null;

    // Start is called before the first frame update
    void Start()
    {
        timeRanking = timeRanking.GetComponent<TimeRanking>();
        timeRanking.SetUpTimeRanking("TimeAttack", 10, 3);

        timeCounter = timeCounter.GetComponent<TimeCount>();
        text = text.GetComponent<Text>();
        text.text = "";

        for(int idx = 0; idx < playerNum; idx++)
        {
            rapCnt[idx] = 1;
        }
    }

    bool isCalledOnce = false;

    bool StartCall  = false;    // StartCountテスト用
    bool FinishCall = false;    // FinishCountテスト用

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
                timeCounter.StartCount();
            }
        }

        // ゴールしたタイミングでFinishCountを呼ぶ
        if (!FinishCall)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                Debug.Log("ラップタイム");
                rapCnt[0]++;    // プレイヤーごとにラップカウントをとる
                timeCounter.RapCount(playerID);
                if (!(rapCnt[0] <= 3))
                {
                    Debug.Log("ゴール");
                    FinishCall = true;
                    timeCounter.FinishCount(playerID);
                    text.text = "ＧＯＡＬ！！";
                }
            }
        }
    }
}
