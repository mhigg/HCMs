using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAttack_result : MonoBehaviour
{
    public DispRanking dispRanking = null;

    // Start is called before the first frame update
    void Start()
    {
        dispRanking = dispRanking.GetComponent<DispRanking>();
        // 第一引数をコース名にする
        dispRanking.SetUpDispRanking("Course2", 11, 3, true, 1000.0f);
    }

    private bool isCalledOnce = false;

    // Update is called once per frame
    void Update()
    {
        if (!isCalledOnce)
        {
            ///ここを任意のボタンにしましょう。
            if (Input.GetKeyDown("space"))
            {
                //タイトルに戻る
                isCalledOnce = true;
                FadeManager.Instance.LoadScene("TitleScene", 2.0f);
                Debug.Log("Titleへ");
                //もしくはメニューに戻る
            }
            if (Input.GetKeyDown("return"))
            {
                isCalledOnce = true;
                FadeManager.Instance.LoadScene("MenuScene", 2.0f);
                Debug.Log("Menuへ");
            }
        }
    }
}
