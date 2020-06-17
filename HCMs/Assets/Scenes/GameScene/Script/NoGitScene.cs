using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoGitScene : MonoBehaviour
{
    public StartStopController startCounter;
    public TimeCount timeCounter;

    public TimeRanking timeRanking;
    public GoalFlag goalFlag;

    private int _playerNum;            // プレイヤー人数

    private string[] _stageNameTbl;
    private int[] _rapMaxTbl;

    void Awake()
    {
        _stageNameTbl = new string[]{
            "NoGitScene",
        };

        _rapMaxTbl = new int[] { 3 };
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("NoGitScene初期化");
        _playerNum = GameObject.FindGameObjectsWithTag("RacingCar").Length;

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
        timeRanking.SetUpTimeRanking(stageName, _playerNum, rapMax);

        goalFlag = goalFlag.GetComponent<GoalFlag>();
        goalFlag.SetUpGoalFlag(rapMax);

        timeCounter = timeCounter.GetComponent<TimeCount>();

        startCounter = startCounter.GetComponent<StartStopController>();
    }

    private bool StartCall = false;   // StartCountテスト用
    bool isCalledOnce = false;

    // Update is called once per frame
    void Update()
    {
        for (int playerID = 0; playerID < _playerNum; playerID++)
        {
            if (goalFlag.CheckGoal(playerID))
            {
                Debug.Log("Spaceキーを押してリザルトへ");
                if (!isCalledOnce)
                {
                    ///ここを任意のボタンにしましょう。
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        isCalledOnce = true;
                        FadeManager.Instance.LoadScene("NoGitResult", 2.0f);
                        Debug.Log("Resultへ");
                    }
                }
            }
        }

        // 特定のタイミングでStartCountを呼ぶ
        if (!StartCall)
        {
            if (!(startCounter.startWait))
            {
                Debug.Log("レーススタート");
                StartCall = true;
                timeCounter.StartCount();
            }
        }
    }
}
