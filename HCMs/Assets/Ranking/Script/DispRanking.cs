using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ランキング表示データ作成・表示
public class DispRanking : MonoBehaviour
{
    public Text rankingText = null;         // ランキングのタイム表示テキスト

    private const string RANKING_KEY = "ranking";   // ランキング呼び出し用キー
    private const int RANK_MAX = 10;                // ランキングの最大保存数
    private float[] ranking = new float[RANK_MAX];  // ランキング保存用（10個まで）

    public RankingStorage rankingStorage = null;

    void Start()
    {
        rankingText = rankingText.GetComponent<Text>();

        // ランキングデータを取得し、表示用データに反映する
        rankingStorage = rankingStorage.GetComponent<RankingStorage>();
        ranking = rankingStorage.GetRankingData(RANKING_KEY, RANK_MAX);
    }

    public void InputText()
    {
    }

    void Update()
    {
        string ranking_string = "";  // ランキング表示用

        string time = "";
        for (int idx = 0; idx < RANK_MAX; idx++)
        {
            time = (ranking[idx] < 1000.0f ? ranking[idx].ToString("f3") + "秒\n" : "―――\n");
            ranking_string = ranking_string + (idx + 1) + "位 " + time;
        }

        // テキストの表示を入れ替える
        rankingText.text = ranking_string;
    }
}
