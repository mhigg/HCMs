using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeAttack_result : MonoBehaviour
{
    public DispRanking dispRanking = null;  // 表示用ランキング用

    private string _activeStageName;    // 現在のステージ名

    // Start is called before the first frame update
    void Start()
    {
        _activeStageName = SceneManager.GetActiveScene().name;

        dispRanking = dispRanking.GetComponent<DispRanking>();

        // 第一引数をコース名にするためにシーン名からResultを消して渡す
        // ランキング表示数は10+ランク外で11、最大周回数はコースごとに異なる
        dispRanking.SetUpDispRanking(_activeStageName.Replace("Result", ""), 6, FindInfoByScene.Instance.GetLapMax(_activeStageName), true, 1000.0f);
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
