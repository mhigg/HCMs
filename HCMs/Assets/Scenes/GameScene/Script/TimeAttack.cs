using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeAttack : MonoBehaviour
{
    //public FadeManager fadeManager = null;
    public TimeCount timeCounter = null;

    public TimeRanking timeRanking = null;
    public DispRanking dispRanking = null;  // 表示用ランキング用
    public GameObject resultCanvas = null;
    public GoalFlag goalFlag = null;

    public StartStopController startCounter = null;

    private string _activeStageName;    // 現在のステージ名

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("TimeAttackStart");
        _activeStageName = SceneManager.GetActiveScene().name;

        FindInfoByScene.Instance.EntryPlayerName();

        int lapMax = FindInfoByScene.Instance.GetLapMax(_activeStageName);
        Debug.Log("lapMax:" + lapMax);

        GameObject[] racingCars = GameObject.FindGameObjectsWithTag("RacingCar");
        for(int idx = 0; idx < FindInfoByScene.Instance.GetPlayerNum; idx++)
        {
            racingCars[idx].GetComponent<LapCount>().SetUp();
            racingCars[idx].GetComponent<CheckPointCount>().SetUp();
        }

        timeRanking = timeRanking.GetComponent<TimeRanking>();
        // 第一引数をコース名にする
        timeRanking.SetUpTimeRanking(_activeStageName, 6, lapMax);

        dispRanking = dispRanking.GetComponent<DispRanking>();

        goalFlag = goalFlag.GetComponent<GoalFlag>();

        timeCounter = timeCounter.GetComponent<TimeCount>();

        startCounter = startCounter.GetComponent<StartStopController>();
    }

    private bool _isCalledOnce = false; // リザルトシーン遷移フラグ
    private bool _startCall = false;    // カウントスタートのフラグ
    private float _afterTime = 0.0f;    // ゴール後タイムカウント

    // Update is called once per frame
    void Update()
    {
        if (goalFlag.CheckFinish())
        {
            Debug.Log("Spaceキーを押してリザルトへ");
            if (!_isCalledOnce)
            {
                if (_afterTime > 10.0f
                 || Input.GetButtonDown("Decision"))
                {
                    Debug.Log("Result");
                    // ランキング表示数は5+ランク外で6、最大周回数はコースごとに異なる
                    dispRanking.SetUpDispRanking(_activeStageName, 6, FindInfoByScene.Instance.GetLapMax(_activeStageName), true, 1000.0f);
                    resultCanvas.SetActive(true);
                    _isCalledOnce = true;
                }

                _afterTime += Time.deltaTime;
            }
            else
            {
                FinishGame();
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

    private void FinishGame()
    {
        if (Input.GetButtonDown("Decision"))
        {
            FadeManager.Instance.LoadScene("MenuScene", 2.0f);
        }
    }
}
