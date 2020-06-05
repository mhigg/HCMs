using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NoGitScene : MonoBehaviour
{
    public TimeCount timeCounter = null;
    public Text text = null;

    const int playerNum = 4;            // プレイヤー人数
    int[] rapCnt = new int[playerNum];  // 人数分のラップカウント
    KeyCode[] playerKeyCode = new KeyCode[playerNum];

    bool StartCall = false;                     // StartCountテスト用
    bool[] isFinished = new bool[playerNum];    // FinishCountテスト用

    public TimeRanking timeRanking = null;

    // Start is called before the first frame update
    void Start()
    {
        timeRanking = timeRanking.GetComponent<TimeRanking>();
        timeRanking.SetUpTimeRanking("Battle", playerNum, 3);

        timeCounter = timeCounter.GetComponent<TimeCount>();
        text = text.GetComponent<Text>();

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
        if (!isCalledOnce)
        {
            ///ここを任意のボタンにしましょう。
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isCalledOnce = true;
                FadeManager.Instance.LoadScene("gitにはあげないResult", 2.0f);
                Debug.Log("Resultへ");
            }
        }

        // 特定のタイミングでStartCountを呼ぶ
        if (!StartCall)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("レーススタート");
                StartCall = true;
                timeCounter.StartCount();
                text.text = "";
            }
        }

        // ゴールしたタイミングでFinishCountを呼ぶ
        for (int idx = 0; idx < playerNum; idx++)
        {
            if (!isFinished[idx])
            {
                if (Input.GetKeyDown(playerKeyCode[idx]))
                {
                    Debug.Log("ラップタイム");
                    rapCnt[idx]++;    // プレイヤーごとにラップカウントをとる
                    timeCounter.RapCount(idx);
                    if (!(rapCnt[idx] <= 3))
                    {
                        Debug.Log("ゴール");
                        isFinished[idx] = true;
                        timeCounter.FinishCount(idx);
                        text.text = (idx + 1).ToString() + "ＧＯＡＬ！！";
                    }
                }
            }
        }
    }
}
