using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Battle_result : MonoBehaviour
{
    public DispRanking dispRanking = null;

    private string _activeStageName;    // 現在のステージ名

    // Start is called before the first frame update
    void Start()
    {
        _activeStageName = SceneManager.GetActiveScene().name;

        dispRanking = dispRanking.GetComponent<DispRanking>();

        // 第一引数はバトルモードの場合Battleで統一
        dispRanking.SetUpDispRanking("Battle", 2, FindInfoByScene.Instance.GetRapMax(_activeStageName), false, 1000.0f);
    }

    private bool _isCalledOnce = false;

    // Update is called once per frame
    void Update()
    {
        if (!_isCalledOnce)
        {
            ///ここを任意のボタンにしましょう。
            if (Input.GetKeyDown("space"))
            {
                //タイトルに戻る
                _isCalledOnce = true;
                FadeManager.Instance.LoadScene("TitleScene", 2.0f);
                Debug.Log("Titleへ");
                //もしくはメニューに戻る
            }
            if (Input.GetKeyDown("return"))
            {
                _isCalledOnce = true;
                FadeManager.Instance.LoadScene("MenuScene", 2.0f);
                Debug.Log("Menuへ");
            }
        }
    }
}
