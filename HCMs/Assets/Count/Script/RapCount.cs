using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 周回数をプレイヤーごとにカウントする
public class RapCount : MonoBehaviour
{
    public TextMeshProUGUI rapText;

    private int _rapCnt;        // ラップカウント
    private int _rapMax;        // 規定ラップ数 最終的にはコースごとに持たせたい

    void Start()
    {
        _rapCnt = 1;
        _rapMax = 3;

        rapText = rapText.GetComponent<TextMeshProUGUI>();
        // 現状、全プレイヤーの画面にプレイヤー１の周回数が反映されている
        // これを各プレイヤーごとにそれぞれの周回数表示に変更する必要がある
        // 画面分割、プレイヤーごとのカメラの設定などの作業中に関わってくると思われるため、
        // それまでは全プレイヤーにプレイヤー１の周回数を表示しておく
        rapText.text = _rapCnt + " / " + _rapMax;
    }

    public void CountRap()
    {
        _rapCnt++;    // プレイヤーごとにラップカウントをとる
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
}
