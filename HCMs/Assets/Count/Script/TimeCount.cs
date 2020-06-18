using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour
{
    // タイム表示用テキストコンポーネント
    public Text timeText = null;
    public Text rapTimeText = null;

    // タイムを保存するためのコンポーネント
    public TimeRanking timeRanking = null;

    private float _timeCount;       // タイムカウント用変数
    private float _rapTimeCount;    // ラップタイムカウント用変数
    private bool _endFlag;          // カウント停止中true カウント中false

    // Start is called before the first frame update
    void Start()
    {
        timeText = timeText.GetComponent<Text>();
        rapTimeText = rapTimeText.GetComponent<Text>();
        timeRanking = timeRanking.GetComponent<TimeRanking>();

        _timeCount = 0.0f;
        _rapTimeCount = 0.0f;
        _endFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_endFlag)
        {
            _timeCount += Time.deltaTime;
            timeText.text = ChangeTimeNotationAndTimeText(_timeCount);

            _rapTimeCount += Time.deltaTime;
            rapTimeText.text = ChangeTimeNotationAndTimeText(_rapTimeCount);
        }
    }

    // 秒数でカウントしているタイムを分・秒・コンマ以下の表示に変換する
    private string ChangeTimeNotationAndTimeText(float time)
    {
        float second = time % 60.0f;
        int minute = Mathf.FloorToInt(time / 60.0f);
        return string.Format("{0:00}.", minute) + string.Format("{0:00.000}", second);
    }
    
    // カウントを開始する
    public void StartCount()
    {
        Debug.Log("カウントスタート");
        _endFlag = false;
        _timeCount = 0.0f;
        _rapTimeCount = 0.0f;
    }

    // ラップタイムを保存する
    public void RapCount(int playerID)
    {
        Debug.Log("ラップタイム" + _timeCount);
        timeRanking.SetRapTime(_timeCount, playerID);
        //Instantiate(rapTimeText, rapTimeText.transform.position, rapTimeText.transform.rotation);
        //rapTimeText.transform.position.Set(rapTimeText.transform.position.x, 80.0f, rapTimeText.transform.position.z);
        _rapTimeCount = 0.0f;
    }

    // カウントを停止する
    public void FinishCount(int playerID)
    {
        Debug.Log("カウントストップ");
        //_endFlag = true;  // 全員ゴールしたらtrueにするようにしたい
        timeRanking.SetGoalTime(playerID);
    }
}
