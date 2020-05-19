using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour
{
    // タイム表示用テキストコンポーネント
    public Text timeText = null;

    private float _timeCount = 0.0f;    // タイムカウント用変数
    private bool _endFlag = false;      // カウント中true カウント停止時false


    // Start is called before the first frame update
    void Start()
    {
        timeText = timeText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // 毎フレームごとにタイム加算
        // 現在 確認用にTime.timeを使用中
        // 後からtimeCountを使用したタイム計算に変更
        if(Time.time <= 5)
        {
            timeText.text = (Time.time >= 0 ? Time.time.ToString("f3") : "0.000");
        }
        else
        {
            // 仮にTime.timeが5を超えたらタイム計測を停止する
            timeText.text = "5";
            FinishCount();
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
