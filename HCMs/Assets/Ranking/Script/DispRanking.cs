using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ランキング表示データ作成・表示
public class DispRanking : MonoBehaviour
{
    public Text rankingText = null;     // ランキングのタイム表示テキスト

    private float[] dispRanking;    // ランキング保存用
    private float[] dispRapTime;    // ラップタイム保存用

    public DataStorage rankingStorage = null;       // 表示用ランキングの取得用

    private string _rankingKey;     // ゴールタイムのランキング呼び出しキー
    private string _rapRankKey;     // ラップタイムのランキング呼び出しキー
    private int _rankingMax;        // ランキングの表示数(=プレイヤー人数)
    private int _rapRankMax;        // ラップタイムランキングの表示数(=人数*周回数)

    void Start()
    {
        rankingText = rankingText.GetComponent<Text>();

        // ランキングデータを取得し、表示用データに反映する
        // バトルとタイムアタックで違うデ―タを取得する
        rankingStorage = rankingStorage.GetComponent<DataStorage>();
    }

    public void SetUpDispRanking(string gameMode, int playerNum, int rapMax)
    {
        Debug.Log("DispRankingセットアップ");

        _rankingKey = gameMode;
        _rankingMax = playerNum;
        _rapRankMax = playerNum * rapMax;
        _rapRankKey = (gameMode == "TimeAttack" ? "TARap" : "BTRap");

        // あとで利用するかも
        // しなかったら消します
        dispRanking = new float[playerNum];
        dispRanking = rankingStorage.GetData(_rankingKey, playerNum, 1000.0f);

        dispRapTime = new float[_rapRankMax];
        dispRapTime = rankingStorage.GetData(_rapRankKey, _rapRankMax, 1000.0f);
    }

    public void InputText()
    {
    }

    void Update()
    {
        string ranking_string = "";  // ランキング表示用

        // ランキング表示
        string time;
        for (int idx = 0; idx < _rankingMax; idx++)
        {
            time = (dispRanking[idx] < 1000.0f ? dispRanking[idx].ToString("f3") + "秒\n" : "―――\n");
            ranking_string = ranking_string + (idx + 1) + "位 " + time;
        }

        // ラップタイム表示
        // 保存されているラップタイムの確認のために使用
        // １位：〇〇秒　〇〇秒　〇〇秒　　みたいに表示する
        // タイムが無い場合は
        // １位：－－秒　－－秒　－－秒　　といった感じで表示する
        //string rap = "";
        //for (int idx = 0; idx < _rapRankMax; idx++)
        //{
        //    // とりあえず仮に縦にずらーっと表示
        //    // ラップタイム保存確認のため
        //    rap = (dispRapTime[idx] < 1000.0f ? dispRapTime[idx].ToString("f3") + "秒\n" : "―――\n");
        //    ranking_string = ranking_string + (idx + 1) + "位 " + rap;

        //    //rap += (dispRapTime[idx] > 0.0f ? dispRapTime[idx].ToString("f3") + "秒　" : "――秒　");
        //    ////if(dispRapTime[idx] > 0.0f)
        //    ////{
        //    ////    rap += dispRapTime[idx].ToString("f3") + "秒　";
        //    ////}
        //    //if (idx > 0 && (idx + 1) % 3 == 0)
        //    //{
        //    //    ranking_string += ranking_string + (idx / 3) + "位 " + rap + "\n";
        //    //}
        //}

        // テキストの表示を入れ替える
        rankingText.text = ranking_string;
    }
}
