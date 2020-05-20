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
        //fadeManager = fadeManager.GetComponent<FadeManager>();
        timeCounter = timeCounter.GetComponent<TimeCount>();
    }

    bool isCalledOnce = false;

    bool StartCall  = false;     // StartCountテスト用
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
                //fadeManager.LoadScene("TimeAttackResult", 2.0f);
                FadeManager.Instance.LoadScene("TimeAttackResult", 2.0f);
                Debug.Log("Resultへ");
            }
        }

        // 特定のタイミングでStartCountを呼ぶ
        if(!StartCall)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCall = true;
                timeCounter.StartCount();
            }
        }

        // ゴールしたタイミングでFinishCountを呼ぶ
        if (!FinishCall)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                FinishCall = true;
                timeCounter.FinishCount();
            }
        }
    }

    // 保留中
    // SceneManagerを使う必要あり
    //private void ResultSceneLoaded(Scene next, LoadSceneMode mode)
    //{
    //    // 次のシーンに入ったら実行される処理

    //    var gameManager = GameObject.FindWithTag("TimeCounter").GetComponent<TimeCount>();

    //    // データを渡す処理
    //    //gameManager.SetTime(100);

    //    // イベントから削除
    //    SceneManager.sceneLoaded -= ResultSceneLoaded;
    //}
}
