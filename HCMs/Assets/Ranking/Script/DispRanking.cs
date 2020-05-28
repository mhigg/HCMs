using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ランキング表示データ作成・表示
public class DispRanking : MonoBehaviour
{
    public bool isTimeAttackMode;       // TimeAttackならtrue Battleならfalse

    public Text rankingText = null;     // ランキングのタイム表示テキスト

    private const string RANKING_KEY = "timeAttack";    // ランキング呼び出し用キー
    private const int RANK_MAX = 10;                    // ランキングの最大保存数

    private const int RAP_MAX = 3;                      // ラップタイムの最大保存数

    private const string RAP_RANK_KEY = "raprank";          // ラップタイムのランキング呼び出しキー
    private const int RAP_RANK_MAX = RANK_MAX * RAP_MAX;    // ラップタイムのランキングの最大保存数

    private float[] dispRanking;    // ランキング保存用
    private float[] dispRapTime;    // ラップタイム保存用

    public DataStorage rankingStorage = null;       // 表示用ランキングの取得用

    void Start()
    {
        rankingText = rankingText.GetComponent<Text>();

        // ランキングデータを取得し、表示用データに反映する
        // バトルとタイムアタックで違うデ―タを取得する
        rankingStorage = rankingStorage.GetComponent<DataStorage>();

        isTimeAttackMode = true;

        // あとで利用するかも
        // しなかったら消します
        if(isTimeAttackMode)
        {
            dispRanking = new float[RANK_MAX];
            dispRanking = rankingStorage.GetData(RANKING_KEY, RANK_MAX, 1000.0f);

            dispRapTime = new float[RAP_RANK_MAX];
            dispRapTime = rankingStorage.GetData(RAP_RANK_KEY, RAP_RANK_MAX, 1000.0f);
        }
        else
        {
            dispRanking = new float[RANK_MAX];
            dispRanking = rankingStorage.GetData(RANKING_KEY, RANK_MAX, 1000.0f);

            dispRapTime = new float[RAP_RANK_MAX];
            dispRapTime = rankingStorage.GetData(RAP_RANK_KEY, RAP_RANK_MAX, 1000.0f);
        }
    }

    public void InputText()
    {
    }

    void Update()
    {
        string ranking_string = "";  // ランキング表示用

        // ランキング表示
        string time;
        for (int idx = 0; idx < RANK_MAX; idx++)
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
        //for (int idx = 0; idx < RAP_RANK_MAX; idx++)
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
