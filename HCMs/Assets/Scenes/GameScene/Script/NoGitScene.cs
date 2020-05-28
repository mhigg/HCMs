using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NoGitScene : MonoBehaviour
{
    public TimeCount timeCounter = null;
    public Text text = null;

    const int playerNum = 4;
    int[] rapCnt = new int[playerNum];  // 人数分のラップカウント
    KeyCode[] playerKeyCode = new KeyCode[playerNum];

    bool StartCall = false;    // StartCountテスト用
    bool[] isFinished = new bool[playerNum];

    // Start is called before the first frame update
    void Start()
    {
        timeCounter = timeCounter.GetComponent<TimeCount>();
        text = text.GetComponent<Text>();

        for (int idx = 0; idx < playerNum; idx++)
        {
            rapCnt[idx] = 1;
            isFinished[idx] = false;
        }

        playerKeyCode[0] = KeyCode.H;
        playerKeyCode[1] = KeyCode.K;
        playerKeyCode[2] = KeyCode.B;
        playerKeyCode[3] = KeyCode.N;
    }

    // Update is called once per frame
    void Update()
    {
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
                    timeCounter.RapCount(idx.ToString());
                    if (!(rapCnt[idx] <= 3))
                    {
                        Debug.Log("ゴール");
                        isFinished[idx] = true;
                        timeCounter.FinishCount(idx.ToString());
                        text.text = idx.ToString() + "ＧＯＡＬ！！";
                    }
                }
            }
        }
    }
}
