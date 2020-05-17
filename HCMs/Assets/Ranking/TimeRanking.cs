using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class TimeRanking : MonoBehaviour
{
    public InputField rankingLimit = null;  // ランキング表示数の限界
    public Text limitText = null;           // 表示数の限界数
    public Text timeText = null;            // ランキングのタイム表示テキスト

    public InputField testScore = null;     // スコア入力用（テスト）
    public Text testScoreText = null;
    
    private const string RANKING_KEY = "ranking";   // ランキング呼び出し用キー
    private const int RANK_MAX = 10;                // ランキングの最大保存数
    private float[] ranking = new float[RANK_MAX];  // ランキング保存用（10個まで）

    void Start()
    {
        //Componentを扱えるようにする
        rankingLimit = rankingLimit.GetComponent<InputField>();
        limitText = limitText.GetComponent<Text>();

        testScore = testScore.GetComponent<InputField>();
        testScoreText = testScoreText.GetComponent<Text>();

        // オブジェクトからTextコンポーネントを取得
        timeText = timeText.GetComponent<Text>();

        for (int idx = 0; idx < ranking.Length; idx++)
        {
            ranking[idx] = 999.0f;
        }
    }

    public void InputText()
    {
        //テキストにinputFieldの内容を反映
        limitText.text = rankingLimit.text;
    }

    // 新しく計測されたタイムをPlayerPrefsに保存
    public void SetNewTime()
    {
        testScoreText.text = testScore.text;
        float newTime = float.Parse(testScoreText.text);
        if (ranking.Length > 0)
        {
            float tmp = 0.0f;
            for (int idx = 0; idx < ranking.Length; idx++)
            {
                // 降順
                if (ranking[idx] > newTime)
                {
                    // 1つずつ順位をずらしていく
                    tmp = ranking[idx];
                    ranking[idx] = newTime;
                    newTime = tmp;
                }
           }
        }
        else
        {
            ranking[0] = newTime;
        }
        // 配列を文字列に変換して PlayerPrefs に格納
        string ranking_string = string.Join(",", ranking);
        PlayerPrefs.SetString(RANKING_KEY, ranking_string);
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
        timeText.text = ranking_string;
    }

    // PlayerPrefsに保存したランキングをranking配列に格納
    public void GetRanking()
    {
        string _ranking = PlayerPrefs.GetString(RANKING_KEY);
        if (_ranking.Length > 0)
        {
            string[] time = _ranking.Split(","[0]);
            ranking = new float[RANK_MAX];
            for(int idx = 0; idx < time.Length && idx < RANK_MAX; idx++)
            {
                ranking[idx] = float.Parse(time[idx]);
            }
        }
    }

    // 新しく計測されたタイムをPlayerPrefsに保存
    public void SetNewTime(float newTime)
    {
        if(ranking.Length > 0)
        {
            float tmp = 0.0f;
            for(int idx = 0; idx < ranking.Length; idx++)
            {
                // 降順
                if(ranking[idx] < newTime)
                {
                    // 1つずつ順位をずらしていく
                    tmp = ranking[idx];
                    ranking[idx] = newTime;
                    newTime = tmp;
                }
            }    
        }
        else
        {
            ranking[0] = newTime;
        }
        // 配列を文字列に変換して PlayerPrefs に格納
        string ranking_string = string.Join(",", ranking);
        PlayerPrefs.SetString(RANKING_KEY, ranking_string);
    }

    // ランキングの削除
    void DeleteRanking()
    {
        if(ranking.Length > 0)
        {
            PlayerPrefs.DeleteKey(RANKING_KEY);
        }
    }
}
