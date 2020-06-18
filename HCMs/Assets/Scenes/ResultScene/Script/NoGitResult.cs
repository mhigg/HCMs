using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoGitResult : MonoBehaviour
{
    public DispRanking dispRanking = null;

    // Start is called before the first frame update
    void Start()
    {
        dispRanking = dispRanking.GetComponent<DispRanking>();

        // ランキング表示数はレースに参加した人数(CPU込)、最大周回数はコースごとに異なる
        dispRanking.SetUpDispRanking("Battle", 2, 3, false, 1000.0f);
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
