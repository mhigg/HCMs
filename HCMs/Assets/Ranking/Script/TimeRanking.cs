using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ランキング集計・保存
public class TimeRanking : MonoBehaviour
{
    private const string RANKING_KEY = "timeAttack";    // ランキング呼び出し用キー
    private const int RANK_MAX = 10;                    // ランキングの最大保存数

    private const int RAP_MAX = 3;                      // ラップタイムの最大保存数

    private const string RAP_RANK_KEY = "raprank";          // ラップタイムのランキング呼び出しキー
    private const int RAP_RANK_MAX = RANK_MAX * RAP_MAX;    // ラップタイムのランキングの最大保存数

    public DataStorage storage = null;                      // ランキング保管スクリプト

    void Start()
    {
        storage = storage.GetComponent<DataStorage>();
    }

    // プレイヤーごとにラップタイムを保存
    // @newTime float:周回時のタイム
    // @playerKey string:プレイヤーID
    public void SetRapTime(float newRapTime, string playerKey)
    {
        Debug.Log("SetRapTimeバトルモード");
        AddRapTime(playerKey, RAP_MAX, newRapTime);
    }

    // ラップタイムを集計していく関数
    // １ラップ終了ごとに呼び出す
    // @KEY string:読み出すデータのキー
    // @DATA_MAX int:読み出すデータ数（配列の最大値）
    // @newTime float:新しく追加するデータ
    private void AddRapTime(string KEY, int DATA_MAX, float newTime)
    {
        float[] rapTime = storage.GetData(KEY, DATA_MAX, 0.0f);

        for (int idx = 0; idx < rapTime.Length; idx++)
        {
            if (rapTime[idx] == 0.0f)
            {
                // データが無かったらラップタイムを入れ、breakする
                // newTimeはレース通してのタイムなので、1つ前のタイムとの差をラップタイムとする
                // 1周目だけはそのままのタイムをラップタイムとする
                rapTime[idx] = (idx == 0 ? newTime : newTime - rapTime[idx - 1]);
                break;
            }
        }

        // 配列を文字列に変換して PlayerPrefs に格納
        string raptime_string = string.Join(",", rapTime);
        PlayerPrefs.SetString(KEY, raptime_string);
    }

    // ラップタイムとゴールタイムを集計しランキングに保存する
    // プレイヤーごとにゴール時に呼び出す
    public void SetGoalTime(string playerKey)
    {
        Debug.Log("SetGoalTime");
        float[] rapTime = storage.GetData(playerKey, RAP_MAX, 0.0f);
        float goalTime = 0.0f;
        for (int idx = 0; idx < RAP_MAX; idx++)
        {
            goalTime += rapTime[idx];
        }

        AddAndSortRapTimeRanking(RAP_RANK_KEY, RAP_RANK_MAX, rapTime);
        AddAndSortGoalTimeRanking(RANKING_KEY, RANK_MAX, goalTime);

        storage.DeleteData(playerKey);
    }

    // ゴールタイムを保存しランキングに反映する関数
    // @KEY string:読み出すランキングのキー
    // @DATA_MAX int:読み出すデータ数（配列の最大値）
    // @newTime float:新しく追加するタイム
    private void AddAndSortGoalTimeRanking(string KEY, int DATA_MAX, float newTime)
    {
        float[] ranking = storage.GetData(KEY, DATA_MAX, 1000.0f);

        if (ranking != null)
        {
            float tmp;
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
        PlayerPrefs.SetString(KEY, ranking_string);
    }

    // 周回数分のラップタイムを保存しランキングに反映する関数
    // @KEY string:読み出すランキングのキー
    // @DATA_MAX int:読み出すデータ数（配列の最大値）
    // @newTime float[]:新しく追加するタイム(周回数分の配列)
    private void AddAndSortRapTimeRanking(string KEY, int DATA_MAX, float[] newTime)
    {
        float[] ranking = storage.GetData(KEY, DATA_MAX, 1000.0f);

        if (ranking != null)
        {
            float tmp;
            for (int idx = 0; idx < ranking.Length; idx += RAP_MAX)
            {
                float retTime = 0.0f;
                float goalTime = 0.0f;
                for (int sumIdx = 0; sumIdx < RAP_MAX; sumIdx++)
                {
                    retTime += ranking[idx + sumIdx];
                    goalTime += newTime[sumIdx];
                }
                
                // 降順
                if (retTime > goalTime)
                {
                    // １つずつ順位をずらしていく
                    // 最大ラップ数ごとに１つとして扱う
                    for(int sortIdx = 0; sortIdx < RAP_MAX; sortIdx++)
                    {
                        tmp = ranking[idx + sortIdx];
                        ranking[idx + sortIdx] = newTime[sortIdx];
                        newTime[sortIdx] = tmp;
                    }
                }
            }
        }
        else
        {
            for (int idx = 0; idx < RAP_MAX; idx++)
            {
                ranking[idx] = newTime[idx];
            }
        }

        // 配列を文字列に変換して PlayerPrefs に格納
        string rapRanking_string = string.Join(",", ranking);
        PlayerPrefs.SetString(KEY, rapRanking_string);
    }
}
