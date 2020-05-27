using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ランキング集計・保存
public class TimeRanking : MonoBehaviour
{
    public InputField inputTime = null;             // タイム入力用(デバッグ用)
    public InputField inputRap = null;              // ラップタイム入力用(デバッグ用)

    private int rapCnt = 1;                         // デバッグ用ラップカウント

    private const string RANKING_KEY = "timeAttack";   // ランキング呼び出し用キー
    private const int RANK_MAX = 10;                // ランキングの最大保存数

    private const string RAPTIME_KEY = "raptime";   // ラップタイム呼び出し用キー
    private const int RAP_MAX = 3;                  // ラップタイムの最大保存数

    private const string RAP_RANK_KEY = "raprank";          // ラップタイムのランキング呼び出しキー
    private const int RAP_RANK_MAX = RANK_MAX * RAP_MAX;    // ラップタイムのランキングの最大保存数

    public DataStorage storage = null;              // ランキング保管スクリプト

    void Start()
    {
        inputTime = inputTime.GetComponent<InputField>();
        inputRap = inputRap.GetComponent<InputField>();

        rapCnt = 1;

        storage = storage.GetComponent<DataStorage>();
    }

    // 新しく計測されたラップタイムをPlayerPrefsに保存
    // Textコンポーネントから数値を読み取る(手入力)
    // Debug用
    public void SetRapTime()
    {
        Debug.Log("SetRapTime引数なし");
        rapCnt++;
        float newRapTime = float.Parse(inputRap.text);
        AddRapTime(RAPTIME_KEY, RAP_MAX, newRapTime);
        if (!(rapCnt <= RAP_MAX))
        {
            SetGoalTime();
        }
    }

    // 新しく計測されたラップタイムをPlayerPrefsに保存
    // 引数から数値を読み取る
    public void SetRapTime(float newRapTime)
    {
        Debug.Log("SetRapTime引数あり");
        AddRapTime(RAPTIME_KEY, RAP_MAX, newRapTime);
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
                rapTime[idx] = newTime;
                break;
            }
        }

        // 配列を文字列に変換して PlayerPrefs に格納
        string raptime_string = string.Join(",", rapTime);
        PlayerPrefs.SetString(KEY, raptime_string);
    }

    // 新しく計測されたタイムをPlayerPrefsに保存
    // Textコンポーネントから数値を読み取る(手入力)
    // Debug用
    public void SetNewTime()
    {
        Debug.Log("SetNewTime引数なし");
        float newTime = float.Parse(inputTime.text);
        AddAndSortGoalTimeRanking(RANKING_KEY, RANK_MAX, newTime);
    }

    // ラップタイムとゴールタイムを集計しランキングに保存する
    // レース終了時に呼び出す
    public void SetGoalTime()
    {
        Debug.Log("SetGoalTime");
        float[] rapTime = storage.GetData(RAPTIME_KEY, RAP_MAX, 0.0f);
        float goalTime = 0.0f;
        for (int idx = 0; idx < RAP_MAX; idx++)
        {
            goalTime += rapTime[idx];
        }

        AddAndSortRapTimeRanking(RAP_RANK_KEY, RAP_RANK_MAX, rapTime);
        AddAndSortGoalTimeRanking(RANKING_KEY, RANK_MAX, goalTime);

        storage.DeleteData(RAPTIME_KEY);
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
