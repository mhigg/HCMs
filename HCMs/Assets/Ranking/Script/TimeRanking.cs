using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ランキング集計・保存
public class TimeRanking : MonoBehaviour
{
    public DataStorage storage = null;  // ランキング保管スクリプト

    private string _rankingKey;     // ゴールタイムのランキング呼び出しキー
    private string _rapRankKey;     // ラップタイムのランキング呼び出しキー
    private int _rankingMax;        // ランキングの表示数(=プレイヤー人数)
    private int _rapMax;            // ラップタイム数
    private int _rapRankMax;        // ラップタイムランキングの表示数(=人数*周回数)

    void Start()
    {
        storage = storage.GetComponent<DataStorage>();
    }
    
    // @gameMode string型：ゲームモード
    // @indicateRanks int型：表示するランキング数
    // @rapMax int型：最大周回数
    public void SetUpTimeRanking(string gameMode, int indicateRanks, int rapMax)
    {
        Debug.Log("TimeRankingセットアップ");

        _rankingKey = gameMode;
        _rapMax = rapMax;
        _rankingMax = indicateRanks;
        _rapRankMax = indicateRanks * rapMax;

        if (gameMode == "Battle")
        {
            _rapRankKey = "BTRap";
            storage.DeleteData(_rankingKey);    // バトルモードのラップタイムランキングは持ち越さないので削除
            for (int playerID = 0; playerID < indicateRanks; playerID++)
            {
                // 前回のラップタイムが保存されたままにならないように削除しておく
                storage.DeleteData(playerID.ToString());
            }
        }
        else
        {
            _rapRankKey = "TARap";
            // 前回のラップタイムが保存されたままにならないように削除しておく
            storage.DeleteData("0");
        }
    }

    // プレイヤーごとにラップタイムを保存
    // @newTime float:周回時のタイム
    // @playerKey string:プレイヤーID
    public void SetRapTime(float newRapTime, int playerKey)
    {
        Debug.Log("SetRapTime");
        AddRapTime(playerKey.ToString(), _rapMax, newRapTime);
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
                // newTimeはレース通してのタイムなので、1つ前までのタイムとの差をラップタイムとする
                // 1周目だけはそのままのタイムをラップタイムとする
                float subTime = 0.0f;
                for(int subIdx = idx - 1; subIdx >= 0; subIdx--)
                {
                    subTime += rapTime[subIdx];
                }
                rapTime[idx] = newTime - subTime;
                break;
            }
        }

        storage.SaveData(KEY, rapTime);
    }

    // ラップタイムとゴールタイムを集計しランキングに保存する
    // プレイヤーごとにゴール時に呼び出す
    public void SetGoalTime(int playerKey)
    {
        Debug.Log("SetGoalTime");
        float[] rapTime = storage.GetData(playerKey.ToString(), _rapMax, 0.0f);
        float goalTime = 0.0f;
        for (int idx = 0; idx < _rapMax; idx++)
        {
            goalTime += rapTime[idx];
        }

        AddAndSortRapTimeRanking(_rapRankKey, _rapRankMax, rapTime);
        AddAndSortGoalTimeRanking(_rankingKey, _rankingMax, goalTime);

        storage.DeleteData(playerKey.ToString());
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

        storage.SaveData(KEY, ranking);
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
            for (int idx = 0; idx < ranking.Length; idx += _rapMax)
            {
                float retTime = 0.0f;
                float goalTime = 0.0f;
                for (int sumIdx = 0; sumIdx < _rapMax; sumIdx++)
                {
                    retTime += ranking[idx + sumIdx];
                    goalTime += newTime[sumIdx];
                }
                
                // 降順
                if (retTime > goalTime)
                {
                    // １つずつ順位をずらしていく
                    // 最大ラップ数ごとに１つとして扱う
                    for(int sortIdx = 0; sortIdx < _rapMax; sortIdx++)
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
            for (int idx = 0; idx < _rapMax; idx++)
            {
                ranking[idx] = newTime[idx];
            }
        }

        storage.SaveData(KEY, ranking);
    }
}
