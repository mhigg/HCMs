using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// 周回数をプレイヤーごとにカウントする
public class LapCount : MonoBehaviour
{
    //public TextMeshProUGUI lapText;     // 周回数表示

    private int _lapCnt;        // ラップカウント
    private int _lapMax;        // 最大周回数

    public void SetUp()
    {
        Debug.Log("LapCountStart");
        _lapCnt = 1;    // 初期値は１
        _lapMax = FindInfoByScene.Instance.GetLapMax(SceneManager.GetActiveScene().name);

        //lapText = lapText.GetComponent<TextMeshProUGUI>();
        //lapText.text = _lapCnt + " / " + _lapMax;
    }

    public void CountLap()
    {
        _lapCnt++;
        //if (_lapCnt <= _lapMax)
        //{
        //    lapText.text = _lapCnt + " / " + _lapMax;
        //}
        //Debug.Log(lapText.text);
    }

    public bool CheckLapCount()
    {
        return !(_lapCnt <= _lapMax);
    }

    public int GetLapCount()
    {
        return _lapCnt;
    }
}
