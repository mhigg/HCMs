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
    private bool _isTimeAttack;     // タイムアタックならtrue バトルならfalse

    private Vector3 _drawOffset;    // ラップタイム表示用のオフセット

    // Start is called before the first frame update
    void Start()
    {
        timeText = timeText.GetComponent<Text>();
        rapTimeText = rapTimeText.GetComponent<Text>();
        timeRanking = timeRanking.GetComponent<TimeRanking>();

        _timeCount = 0.0f;
        _rapTimeCount = 0.0f;
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
            timeText.text = ChangeTimeNotationAndTimeText(_timeCount);

            _rapTimeCount += Time.deltaTime;
        }
    }

    // 秒数でカウントしているタイムを分・秒・コンマ以下の表示に変換する
    private string ChangeTimeNotationAndTimeText(float time)
    {
        float second = time % 60.0f;
        int minute = Mathf.FloorToInt(time / 60.0f);
        return string.Format("{0:00}.", minute) + string.Format("{0:00.000}", second);
    }

    // ラップタイムテキストをインスタンス
    // 周回時に呼ぶ。タイムアタックのみ
    private void InstantiateRapTimeText()
    {
        int rapCnt = GameObject.FindWithTag("RacingCar").GetComponent<RapCount>().GetRapCount();
        Vector3 position = new Vector3(645 + _drawOffset.x, (400 - 80 * rapCnt) + _drawOffset.y, _drawOffset.z);
        Debug.Log("position:" + position);

        Text _rapTimeText = Instantiate(rapTimeText, position, Quaternion.identity);
        _rapTimeText.text = "RAP" + (rapCnt - 1) + "   " + ChangeTimeNotationAndTimeText(_rapTimeCount);
        _rapTimeText.transform.SetParent(GameObject.Find("CounterCanvas").transform);
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
        if(_isTimeAttack)
        {
            InstantiateRapTimeText();
        }
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
