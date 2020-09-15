using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Battle : MonoBehaviour
{
    public StartStopController startCounter;    // スタートカウント用
    public TimeCount timeCounter;               // タイムカウント用
    
    public TimeRanking timeRanking;             // ランキング記録用
    public DispRanking dispRanking = null;      // 表示用ランキング用
    public GameObject resultCanvas = null;
    public GoalFlag goalFlag;                   // ゴール判定取得用

    private string _activeStageName;    // 現在のステージ名

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("バトルシーン初期化");
        int _playerNum = FindInfoByScene.Instance.GetPlayerNum;

        _activeStageName = SceneManager.GetActiveScene().name;

        int lapMax = FindInfoByScene.Instance.GetLapMax(_activeStageName);
        Debug.Log("lapMax:" + lapMax);

        GameObject[] racingCars = GameObject.FindGameObjectsWithTag("RacingCar");
        for (int idx = 0; idx < _playerNum; idx++)
        {
            racingCars[idx].GetComponent<LapCount>().SetUp();
            racingCars[idx].GetComponent<CheckPointCount>().SetUp();
        }

        timeRanking = timeRanking.GetComponent<TimeRanking>();
        timeRanking.SetUpTimeRanking("Battle", _playerNum, lapMax);

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
                    dispRanking.SetUpDispRanking("Battle", FindInfoByScene.Instance.GetPlayerNum, FindInfoByScene.Instance.GetLapMax(_activeStageName), false, 1000.0f);
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
