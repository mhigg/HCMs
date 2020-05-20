using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ランキング表示データ作成・表示
public class DispRanking : MonoBehaviour
{
    public InputField rankingLimit = null;  // ランキング表示数の限界
    public Text limitText = null;           // 表示数の限界数
    public Text rankingText = null;         // ランキングのタイム表示テキスト

    private const string RANKING_KEY = "ranking";   // ランキング呼び出し用キー
    private const int RANK_MAX = 10;                // ランキングの最大保存数
    private float[] ranking = new float[RANK_MAX];  // ランキング保存用（10個まで）

    public GetRanking getRanking = null;

    void Start()
    {
        //Componentを扱えるようにする
        rankingLimit = rankingLimit.GetComponent<InputField>();
        limitText = limitText.GetComponent<Text>();

        rankingText = rankingText.GetComponent<Text>();

        // ランキングデータを取得し、表示用データに反映する
        getRanking = getRanking.GetComponent<GetRanking>();
        ranking = getRanking.GetRankingData(RANKING_KEY, RANK_MAX);
    }

    public void InputText()
    {
        //テキストにinputFieldの内容を反映
        limitText.text = rankingLimit.text;
    }

    void Update()
    {
        string ranking_string = "";  // ランキング表示用

        int limit = int.Parse(limitText.text);
        //using System;
        //int limit = 0;
        //Int32.TryParse(limitText.text, out limit);

        for (int idx = 0; idx < limit; idx++)
        {
            ranking_string = ranking_string + (idx + 1) + "位 " + ranking[idx] + "秒\n";
        }

        // テキストの表示を入れ替える
        rankingText.text = ranking_string;
    }
}
