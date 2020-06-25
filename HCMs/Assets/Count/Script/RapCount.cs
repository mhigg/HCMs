using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 周回数をプレイヤーごとにカウントする
public class RapCount : MonoBehaviour
{
    public TextMeshProUGUI rapText;     // 周回数表示

    private int _rapCnt;        // ラップカウント
    private int _rapMax;        // 最大周回数　コースごとに取得できるようにする

    void Start()
    {
        _rapCnt = 1;    // 初期値は１
        _rapMax = 3;    // 現状すべて3周なので3を入れておく    // ※RAPMAX※

        rapText = rapText.GetComponent<TextMeshProUGUI>();
        rapText.text = _rapCnt + " / " + _rapMax;
    }

    public void CountRap()
    {
        _rapCnt++;
        if (_rapCnt <= _rapMax)
        {
            rapText.text = (_rapCnt) + " / " + _rapMax;
        }
        Debug.Log(rapText.text);
    }

    public bool CheckRapCount()
    {
        return !(_rapCnt <= _rapMax);
    }

    public int GetRapCount()
    {
        return _rapCnt;
    }
}
