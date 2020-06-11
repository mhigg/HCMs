using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ランキング表示データ作成・表示
public class DispRanking : MonoBehaviour
{
    public Text rankingTimeText = null;     // ランキングのタイム表示テキスト
    public Text rankingOutText = null;      // タイムがランキング外だった際に使用

    public Canvas dispCanvas = null;        // 表示するキャンバス
    public Canvas hiddenCanvas = null;      // 非表示にするキャンバス

    private float[] dispRanking;    // ランキング保存用
    private float[] dispRapTime;    // ラップタイム保存用

    public DataStorage rankingStorage = null;       // 表示用ランキングの取得用

    private string _rankingKey;     // ゴールタイムのランキング呼び出しキー
    private string _rapRankKey;     // ラップタイムのランキング呼び出しキー
    private int _rankingMax;        // ランキングの表示数(=プレイヤー人数)
    private int _rapRankMax;        // ラップタイムランキングの表示数(=人数*周回数)
    private bool _rankOutActive;    // true:ランキング外のタイムを表示する false:表示しない
    private float _defaultTime;     // タイムの初期値もとい限界値

    void Start()
    {
        rankingTimeText = rankingTimeText.GetComponent<Text>();
        rankingOutText = rankingOutText.GetComponent<Text>();

        dispCanvas = dispCanvas.GetComponent<Canvas>();
        hiddenCanvas = hiddenCanvas.GetComponent<Canvas>();

        // ランキングデータを取得し、表示用データに反映する
        // バトルとタイムアタックで違うデ―タを取得する
        rankingStorage = rankingStorage.GetComponent<DataStorage>();
    }

    public void SetUpDispRanking(string gameMode, int playerNum, int rapMax, bool rankOutActive, float defaultTime)
    {
        Debug.Log("DispRankingセットアップ");

        _rankingKey = gameMode;
        _rankingMax = playerNum;
        _rapRankMax = playerNum * rapMax;
        _rapRankKey = (gameMode == "TimeAttack" ? "TARap" : "BTRap");
        _rankOutActive = rankOutActive;
        _defaultTime = defaultTime;

        dispCanvas.gameObject.SetActive(true);
        hiddenCanvas.gameObject.SetActive(false);

        // あとで利用するかも
        // しなかったら消します
        dispRanking = new float[_rankingMax];
        dispRanking = rankingStorage.GetData(_rankingKey, _rankingMax, _defaultTime);

        dispRapTime = new float[_rapRankMax];
        dispRapTime = rankingStorage.GetData(_rapRankKey, _rapRankMax, _defaultTime);
    }

    void Update()
    {
        string ranking_string = "";  // ランキング表示用
        string rankOut_string = "";  // ランキング外表示用

        // ランキング表示
        string time;
        for (int idx = 0; idx < _rankingMax; idx++)
        {
            if(dispRanking[idx] < _defaultTime)
            {
                float second = dispRanking[idx] % 60.0f;
                int minute = Mathf.FloorToInt(dispRanking[idx] / 60.0f);
                time = string.Format("{0:00}.", minute) + string.Format("{0:00.000}", second) + "\n";
            }
            else
            {
                // デフォルト(1000.0f)ならタイム表記を表示しない
                time = "--.--.---\n";
            }

            if (idx == (_rankingMax - 1) && _rankOutActive)
            {
                // ランキング外のタイムを表示する場合、一番下の記録をランキング外として表示する
                rankOut_string += time;

                // ランキング内には表示しないのでbreakする
                break;
            }

            ranking_string += time;
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
        //    rap = (dispRapTime[idx] < _defaultTime ? dispRapTime[idx].ToString("f3") + "秒\n" : "―――\n");
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
        rankingTimeText.text = ranking_string;
        rankingOutText.text = rankOut_string;
    }
}
