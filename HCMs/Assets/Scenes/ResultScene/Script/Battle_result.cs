using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Battle_result : MonoBehaviour
{
    public DispRanking dispRanking = null;

    private string[] _stageNameTbl;     // ステージ名テーブル
    private int[] _rapMaxTbl;           // 最大周回数テーブル

    private string _activeStageName;    // 現在のステージ名

    void Awake()
    {
        _stageNameTbl = new string[]{
            "BattleResult01",
            "BattleResult02",
            "BattleResult03"
        };    // ※STAGENAME※

        _rapMaxTbl = new int[] { 3, 3, 3 };    // ※RAPMAX※
    }

    // Start is called before the first frame update
    void Start()
    {
        _activeStageName = SceneManager.GetActiveScene().name;
        int rapMax = 0;

        for (int idx = 0; idx < _stageNameTbl.Length; idx++)
        {
            if (_stageNameTbl[idx] == _activeStageName)
            {
                rapMax = _rapMaxTbl[idx];
            }
        }

        if (rapMax <= 0)
        {
            Debug.LogError("最大周回数が0以下です。コース情報の照合に失敗した可能性があります。_stageNameTblと_rapMaxを確認してください。");
        }

        dispRanking = dispRanking.GetComponent<DispRanking>();

        // 第一引数はバトルモードの場合Battleで統一
        // それかコース名にしてDispRanking側でコース名をカギにして最大周回数を探査させる？
        // ここはまた別途考える
        // ランキング表示数はレースに参加した人数(CPU込)、最大周回数はコースごとに異なる
        dispRanking.SetUpDispRanking("Battle", 2, rapMax, false, 1000.0f);
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
