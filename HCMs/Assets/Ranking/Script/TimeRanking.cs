using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ランキング集計・保存
public class TimeRanking : MonoBehaviour
{
    public InputField inputTime = null;             // タイム入力用(デバッグ用)

    private const string RANKING_KEY = "ranking";   // ランキング呼び出し用キー
    private const int RANK_MAX = 10;                // ランキングの最大保存数

    private const string RAPTIME_KEY = "raptime";   // ラップタイム呼び出し用キー
    private const int RAP_MAX = 3;                  // ラップタイムの最大保存数

    public DataStorage storage = null;           // ランキング保管スクリプト

    void Start()
    {
        inputTime = inputTime.GetComponent<InputField>();

        storage = storage.GetComponent<DataStorage>();
    }

    // 新しく計測されたタイムをPlayerPrefsに保存
    // Textコンポーネントから数値を読み取る(手入力)
    // Debug用
    public void SetNewTime()
    {
        Debug.Log("SetNewTime引数なし");
        float newTime = float.Parse(inputTime.text);
        float[] ranking = AddAndSortRanking(RANKING_KEY, RANK_MAX, newTime);

        // 配列を文字列に変換して PlayerPrefs に格納
        string ranking_string = string.Join(",", ranking);
        PlayerPrefs.SetString(RANKING_KEY, ranking_string);
    }

    // 新しく計測されたタイムをPlayerPrefsに保存
    // 引数から数値を読み取る
    public void SetNewTime(float newTime)
    {
        Debug.Log("SetNewTime引数あり");
        float[] ranking = AddAndSortRanking(RANKING_KEY, RANK_MAX, newTime);

        // 配列を文字列に変換して PlayerPrefs に格納
        string ranking_string = string.Join(",", ranking);
        PlayerPrefs.SetString(RANKING_KEY, ranking_string);
    }

    // @KEY string:読み出すランキングのキー
    // @DATA_MAX int:読み出すデータ数（配列の最大値）
    // @newTime float:新しく追加するタイム
    private float[] AddAndSortRanking(string KEY, int DATA_MAX, float newTime)
    {
        float[] retRanking = storage.GetData(KEY, DATA_MAX, 1000.0f);

        if (retRanking != null)
        {
            float tmp = 0.0f;
            for (int idx = 0; idx < retRanking.Length; idx++)
            {
                // 降順
                if (retRanking[idx] > newTime)
                {
                    // 1つずつ順位をずらしていく
                    tmp = retRanking[idx];
                    retRanking[idx] = newTime;
                    newTime = tmp;
                }
            }
        }
        else
        {
            retRanking[0] = newTime;
        }
        return retRanking;
    }

    // 新しく計測されたラップタイムをPlayerPrefsに保存
    // Textコンポーネントから数値を読み取る(手入力)
    // Debug用
    public void SetRapTime()
    {
        Debug.Log("SetRapTime引数なし");
        float newRapTime = float.Parse(inputTime.text);
        float[] rapTime = AddRapTime(RAPTIME_KEY, RAP_MAX, newRapTime);

        // 配列を文字列に変換して PlayerPrefs に格納
        string ranking_string = string.Join(",", rapTime);
        PlayerPrefs.SetString(RAPTIME_KEY, ranking_string);
    }


    // 新しく計測されたラップタイムをPlayerPrefsに保存
    // 引数から数値を読み取る
    public void SetRapTime(float newRapTime)
    {
        Debug.Log("SetRapTime引数あり");
        float[] rapTime = AddRapTime(RAPTIME_KEY, RAP_MAX, newRapTime);

        // 配列を文字列に変換して PlayerPrefs に格納
        string ranking_string = string.Join(",", rapTime);
        PlayerPrefs.SetString(RAPTIME_KEY, ranking_string);
    }

    // @KEY string:読み出すデータのキー
    // @DATA_MAX int:読み出すデータ数（配列の最大値）
    // @newTime float:新しく追加するデータ
    private float[] AddRapTime(string KEY, int DATA_MAX, float newTime)
    {
        float[] retRapTime = storage.GetData(KEY, DATA_MAX, 0.0f);

        for (int idx = 0; idx < retRapTime.Length; idx++)
        {
            if (retRapTime[idx] == 0.0f)
            {
                // データが無かったらラップタイムを入れ、breakする
                retRapTime[idx] = newTime;
                break;
            }
        }
        return retRapTime;
    }

}
