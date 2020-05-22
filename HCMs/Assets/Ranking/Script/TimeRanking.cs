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
//    private float[] ranking = new float[RANK_MAX];  // ランキング保存用

    private const string RAPTIME_KEY = "raptime";   // ラップタイム呼び出し用キー
    private const int RAP_MAX = 3;                  // ラップタイムの最大保存数
//    private float[] rapTime = new float[RAP_MAX];   // ラップタイム保存用

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
        float[] ranking = SaveAndSortData(RANKING_KEY, RANK_MAX, newTime);

        // 配列を文字列に変換して PlayerPrefs に格納
        string ranking_string = string.Join(",", ranking);
        PlayerPrefs.SetString(RANKING_KEY, ranking_string);
    }

    // 新しく計測されたタイムをPlayerPrefsに保存
    // 引数から数値を読み取る
    public void SetNewTime(float newTime)
    {
        Debug.Log("SetNewTime引数あり");
        float[] ranking = SaveAndSortData(RANKING_KEY, RANK_MAX, newTime);

        // 配列を文字列に変換して PlayerPrefs に格納
        string ranking_string = string.Join(",", ranking);
        PlayerPrefs.SetString(RANKING_KEY, ranking_string);
    }

    // 新しく計測されたラップタイムをPlayerPrefsに保存
    // Textコンポーネントから数値を読み取る(手入力)
    // Debug用
    public void SetRapTime()
    {
        Debug.Log("SetRapTime引数なし");
        float newRapTime = float.Parse(inputTime.text);
        float[] rapTime = SaveAndSortData(RAPTIME_KEY, RAP_MAX, newRapTime);

        // 配列を文字列に変換して PlayerPrefs に格納
        string ranking_string = string.Join(",", rapTime);
        PlayerPrefs.SetString(RAPTIME_KEY, ranking_string);
    }


    // 新しく計測されたラップタイムをPlayerPrefsに保存
    // 引数から数値を読み取る
    public void SetRapTime(float newRapTime)
    {
        Debug.Log("SetRapTime引数あり");
        float[] rapTime = SaveAndSortData(RAPTIME_KEY, RAP_MAX, newRapTime);

        // 配列を文字列に変換して PlayerPrefs に格納
        string ranking_string = string.Join(",", rapTime);
        PlayerPrefs.SetString(RAPTIME_KEY, ranking_string);
    }

    // @KEY string:読み出すデータのキー
    // @DATA_MAX int:読み出すデータ数（配列の最大値）
    // @newData float:新しく追加するデータ
    private float[] SaveAndSortData(string KEY, int DATA_MAX, float newData)
    {
        float[] retData = storage.GetData(KEY, DATA_MAX);

        if (retData != null)
        {
            float tmp = 0.0f;
            for (int idx = 0; idx < retData.Length; idx++)
            {
                // 降順
                if (retData[idx] > newData)
                {
                    // 1つずつ順位をずらしていく
                    tmp = retData[idx];
                    retData[idx] = newData;
                    newData = tmp;
                }
            }
        }
        else
        {
            retData[0] = newData;
        }
        return retData;
    }
}
