using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour
{
    // タイム表示用テキストコンポーネント
    public Text timeText = null;

    // タイムを保存するためのコンポーネント
    public TimeRanking timeRanking = null;

    private float _timeCount = 0.0f;    // タイムカウント用変数
    private bool _endFlag = true;       // カウント停止中true カウント中false

    // Start is called before the first frame update
    void Start()
    {
        timeText = timeText.GetComponent<Text>();
        timeRanking = timeRanking.GetComponent<TimeRanking>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_endFlag)
        {
            // 毎フレームごとにタイム加算
            // 現在 確認用にTime.timeを使用中
            // 後からtimeCountを使用したタイム計算に変更
            float second = _timeCount % 60.0f;
            int minute = Mathf.FloorToInt(_timeCount / 60.0f);
            timeText.text = string.Format("{0:00}.", minute) + string.Format("{0:00.000}", second);
            _timeCount += Time.deltaTime;
        }
    }
    
    // カウントを開始する
    public void StartCount()
    {
        Debug.Log("カウントスタート");
        _endFlag = false;
        _timeCount = 0.0f;
    }

    // ラップタイムを保存する
    public void RapCount(int playerID)
    {
        Debug.Log("ラップタイムカウント");
        timeRanking.SetRapTime(_timeCount, playerID);
    }

    // カウントを停止する
    public void FinishCount(int playerID)
    {
        Debug.Log("カウントストップ");
        _endFlag = true;  // 全員ゴールしたらtrueにするようにしたい
        timeRanking.SetGoalTime(playerID);
    }
}
