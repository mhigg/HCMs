using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeAttack : MonoBehaviour
{
    //public FadeManager fadeManager = null;
    public TimeCount timeCounter = null;

    public TimeRanking timeRanking = null;
    public GoalFlag goalFlag = null;

    public StartStopController startCounter = null;

    private string[] _stageNameTbl;     // ステージ名テーブル
    private int[] _rapMaxTbl;           // 最大周回数テーブル

    void Awake()
    {
        _stageNameTbl = new string[]{
            "TimeAttack01",
            "TimeAttack02",
            "TimeAttack03"
        };

        _rapMaxTbl = new int[] { 3, 3, 3 };
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("タイムアタック初期化");
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
        // 第一引数をコース名にする
        timeRanking.SetUpTimeRanking(stageName, 11, rapMax);

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
                    FadeManager.Instance.LoadScene("TimeAttackResult", 2.0f);
                    Debug.Log("Resultへ");
                }
            }
        }

        // Debug用　横転等で動けなくなったとき用
        if (!_isCalledOnce)
        {
            ///ここを任意のボタンにしましょう。
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _isCalledOnce = true;
                FadeManager.Instance.LoadScene("TimeAttack02", 2.0f);
                Debug.Log("再スタート");
            }
        }

        // 特定のタイミングでStartCountを呼ぶ
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
