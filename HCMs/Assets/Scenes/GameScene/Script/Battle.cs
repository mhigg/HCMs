﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Battle : MonoBehaviour
{
    public StartStopController startCounter;    // スタートカウント用
    public TimeCount timeCounter;               // タイムカウント用
    
    public TimeRanking timeRanking;             // ランキング記録用
    public GoalFlag goalFlag;                   // ゴール判定取得用

    private string _activeStageName;    // 現在のステージ名

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("バトルシーン初期化");
        int playerNum = GameObject.FindGameObjectsWithTag("RacingCar").Length;

        _activeStageName = SceneManager.GetActiveScene().name;

        int rapMax = FindInfoByScene.Instance.GetRapMax(_activeStageName);
        Debug.Log("rapMax:" + rapMax);

        timeRanking = timeRanking.GetComponent<TimeRanking>();
        timeRanking.SetUpTimeRanking("Battle", playerNum, rapMax);

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
                ///ここを任意のボタンにしましょう。
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    string stageNo = _activeStageName.Substring(_activeStageName.Length - 2);
                    _isCalledOnce = true;
                    FadeManager.Instance.LoadScene("BattleResult" + stageNo, 2.0f);
                    Debug.Log("Result" + stageNo);
                }

                if (_afterTime > 300.0f)
                {
                    string stageNo = _activeStageName.Substring(_activeStageName.Length - 2);
                    _isCalledOnce = true;
                    FadeManager.Instance.LoadScene("BattleResult" + stageNo, 2.0f);
                    Debug.Log("Result" + stageNo);
                }

                _afterTime += Time.deltaTime;
            }
        }

        // Debug用　横転等で動けなくなったとき用
        if (!_isCalledOnce)
        {
            ///ここを任意のボタンにしましょう。
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _isCalledOnce = true;
                FadeManager.Instance.LoadScene(_activeStageName, 2.0f);
                Debug.Log("再スタート");
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
