using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Battle : MonoBehaviour
{
    public StartStopController startCounter;    // スタートカウント用
    public TimeCount timeCounter;               // タイムカウント用
    
    public TimeRanking timeRanking;             // ランキング記録用
    public GoalFlag goalFlag;                   // ゴール判定取得用

    private string[] _stageNameTbl;     // ステージ名テーブル
    private int[] _rapMaxTbl;           // 最大周回数テーブル

    void Awake()
    {
        _stageNameTbl = new string[]{
            "BattleScene_01",
            "BattleScene_02",
            "BattleScene_03"
        };

        _rapMaxTbl = new int[] { 3, 3, 3 };
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("バトルシーン初期化");
        int playerNum = GameObject.FindGameObjectsWithTag("RacingCar").Length;

        string stageName = SceneManager.GetActiveScene().name;
        int rapMax = 0;

        for (int idx = 0; idx < _stageNameTbl.Length; idx++)
        {
            if (_stageNameTbl[idx] == stageName)
            {
                rapMax = _rapMaxTbl[idx];
            }
        }

        if (rapMax <= 0)
        {
            Debug.LogError("最大周回数が0以下です。コース情報の照合に失敗した可能性があります。_stageNameTblと_rapMaxを確認してください。");
        }

        timeRanking = timeRanking.GetComponent<TimeRanking>();
        timeRanking.SetUpTimeRanking("Battle", playerNum, rapMax);

        goalFlag = goalFlag.GetComponent<GoalFlag>();
        goalFlag.SetUpGoalFlag(rapMax);

        timeCounter = timeCounter.GetComponent<TimeCount>();
        startCounter = startCounter.GetComponent<StartStopController>();
    }

    private bool _isCalledOnce = false;  // リザルトシーン遷移フラグ
    private bool _startCall = false;     // カウントスタートのフラグ

    // Update is called once per frame
    void Update()
    {
        if (goalFlag.CheckFinish())
        {
            Debug.Log("Spaceキーを押してリザルトへ");
            if (!_isCalledOnce)
            {
                ///ここを任意のボタンにしましょう。
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _isCalledOnce = true;
                    FadeManager.Instance.LoadScene("BattleResult", 2.0f);
                    Debug.Log("Resultへ");
                }
            }
        }

        if (!_startCall)
        {
            if (!(startCounter.startWait))
            {
                Debug.Log("レーススタート");
                _startCall = true;
                timeCounter.StartCount();
            }
        }
    }
}
