using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ランキング集計・保存
public class TimeRanking : MonoBehaviour
{
    public InputField inputTime = null;     // タイム入力用

    private const string RANKING_KEY = "ranking";   // ランキング呼び出し用キー
    private const int RANK_MAX = 10;                // ランキングの最大保存数
    private float[] ranking = new float[RANK_MAX];  // ランキング保存用（10個まで）

    void Start()
    {
        inputTime = inputTime.GetComponent<InputField>();

        // 仮ランキングデータ初期化
        // 現状だとTimeAttackSceneに入るたびにオールクリアされる
        for(int idx = 0; idx < ranking.Length; idx++)
        {
            ranking[idx] = 999.0f;
        }
    }

    // 新しく計測されたタイムをPlayerPrefsに保存
    // Textコンポーネントから数値を読み取る
    public void SetNewTime()
    {
        float newTime = float.Parse(inputTime.text);
        if (ranking.Length > 0)
        {
            Debug.Log("SetNewTime引数なし");
            float tmp = 0.0f;
            for (int idx = 0; idx < ranking.Length; idx++)
            {
                // 降順
                if (ranking[idx] > newTime)
                {
                    Debug.Log("SetNewTime順位内");
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

    // 新しく計測されたタイムをPlayerPrefsに保存
    // 引数から数値を読み取る
    public void SetNewTime(float newTime)
    {
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

    // ランキングの削除
    void DeleteRanking()
    {
        if (ranking.Length > 0)
        {
            PlayerPrefs.DeleteKey(RANKING_KEY);
        }
    }
}
