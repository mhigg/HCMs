using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour
{
    // タイム表示用テキストコンポーネント
    public Text timeText = null;

    private float _timeCount = 0.0f;    // タイムカウント用変数
    private bool _endFlag = true;      // カウント停止中true カウント中false


    // Start is called before the first frame update
    void Start()
    {
        timeText = timeText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_endFlag)
        {
            // 毎フレームごとにタイム加算
            // 現在 確認用にTime.timeを使用中
            // 後からtimeCountを使用したタイム計算に変更
            timeText.text = (Time.time >= 0 ? Time.time.ToString("f3") : "0.000");
        }
    }
    
    // カウントを開始する
    public void StartCount()
    {
        // タイムを0から開始する
        // _endFlagがtrueになってたらfalseにする
        _endFlag = false;
    }

    // カウントを停止する
    public void FinishCount()
    {
        // タイムカウントを停止する
        // _endFlagをtrueにする
        _endFlag = true;
    }
}
