using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScene : MonoBehaviour
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
            ///タイムアタック
            if (Input.GetKeyDown("space"))
            {
                isCalledOnce = true;
                FadeManager.Instance.LoadScene("TimeAttackScene", 2.0f);
                Debug.Log("TimeAttackへ");
            }
            ///ここを任意のボタンにしましょう。
            ///バトル
            if (Input.GetKeyDown("return"))
            {
                isCalledOnce = true;
                FadeManager.Instance.LoadScene("BattleScene", 2.0f);
                Debug.Log("Battleへ");
            }
            ///ここを任意のボタンにしましょう。
            ///障害物
            if (Input.GetKeyDown("tab"))
            {
                isCalledOnce = true;
                FadeManager.Instance.LoadScene("ObstacleScene", 2.0f);
                Debug.Log("Obへ");
            }
        }
    }
}
