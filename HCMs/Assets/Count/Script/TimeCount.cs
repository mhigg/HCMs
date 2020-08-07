using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour
{
    // タイム表示用テキストコンポーネント
    public Text lapTimeText = null;
    public List<ImageNo> imageNo = new List<ImageNo>();

    // タイムを保存するためのコンポーネント
    public TimeRanking timeRanking = null;

    private float _timeCount;       // タイムカウント用変数
    private float _lapTimeCount;    // ラップタイムカウント用変数
    private bool _endFlag;          // カウント停止中true カウント中false
    private bool _isTimeAttack;     // タイムアタックならtrue バトルならfalse

    private Vector3 _drawOffset;    // ラップタイム表示用のオフセット

    // Start is called before the first frame update
    void Start()
    {
        lapTimeText = lapTimeText.GetComponent<Text>();
        timeRanking = timeRanking.GetComponent<TimeRanking>();
        for(int idx = 0; idx < imageNo.Count; idx++)
        {
            imageNo[idx] = imageNo[idx].GetComponent<ImageNo>();
        }

        _timeCount = 0.0f;
        _lapTimeCount = 0.0f;
        _endFlag = true;
        _isTimeAttack = (GameObject.FindGameObjectsWithTag("RacingCar").Length == 1 ? true : false);

        _drawOffset = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0.0f);
        Debug.Log("オフセット：" + _drawOffset);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_endFlag)
        {
            _timeCount += Time.deltaTime;
            ChangeTimeNotationAndTimeText(_timeCount, true);

            _lapTimeCount += Time.deltaTime;
        }
    }

    // 秒数でカウントしているタイムを分・秒・コンマ以下の表示に変換する
    private string ChangeTimeNotationAndTimeText(float time, bool total)
    {
        float seconds = time % 60.0f;
        int comma = Mathf.FloorToInt((seconds - Mathf.Floor(seconds)) * 1000.0f);
        int second = Mathf.FloorToInt(seconds);
        int minute = Mathf.FloorToInt(time / 60.0f);

        if(total)
        {
            imageNo[0].SetNo(minute, "00");
            imageNo[1].SetNo(second, "00");
            imageNo[2].SetNo(comma, "000");
        }

        return string.Format("{0:00}'", minute) + string.Format("{0:00.000}", seconds);
    }

    // ラップタイムテキストをインスタンス
    // 周回時に呼ぶ。タイムアタックのみ
    private void InstantiateLapTimeText()
    {
        int lapCnt = GameObject.FindWithTag("RacingCar").GetComponent<LapCount>().GetLapCount();
        Vector3 position = new Vector3(645 + _drawOffset.x, (400 - 80 * lapCnt) + _drawOffset.y, _drawOffset.z);
        Debug.Log("position:" + position);

        Text _lapTimeText = Instantiate(lapTimeText, position, Quaternion.identity);
        _lapTimeText.text = "LAP" + (lapCnt - 1) + "   " + ChangeTimeNotationAndTimeText(_lapTimeCount,false);
        _lapTimeText.transform.SetParent(GameObject.Find("TACounterCanvas").transform);
    }

    // カウントを開始する
    public void StartCount()
    {
        Debug.Log("カウントスタート");
        _endFlag = false;
        _timeCount = 0.0f;
        _lapTimeCount = 0.0f;
    }

    // ラップタイムを保存する
    public void LapCount(int playerID)
    {
        Debug.Log("ラップタイム" + _timeCount);
        timeRanking.SetLapTime(_timeCount, playerID);
        if(_isTimeAttack)
        {
            InstantiateLapTimeText();
        }
        _lapTimeCount = 0.0f;
    }

    // カウントを停止する
    public void FinishCount(int playerID)
    {
        Debug.Log("カウントストップ");
        //_endFlag = true;  // 全員ゴールしたらtrueにするようにしたい
        timeRanking.SetGoalTime(playerID);
    }
}
