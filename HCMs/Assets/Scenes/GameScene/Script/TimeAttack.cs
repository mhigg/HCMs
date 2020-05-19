using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class TimeAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool isCalledOnce = false;
    // Update is called once per frame
    void Update()
    {
        if (!isCalledOnce)
        {
            ///ここを任意のボタンにしましょう。
            if (Input.GetKeyDown("space"))
            {
                isCalledOnce = true;
                FadeManager.Instance.LoadScene("TimeAttackResult", 2.0f);
                Debug.Log("Resultへ");
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
