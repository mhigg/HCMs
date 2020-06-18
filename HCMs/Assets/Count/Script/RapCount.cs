using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RapCount : MonoBehaviour
{
    public TextMeshProUGUI rapText;

    private int[] _rapCnt;   // ラップカウント
    private int _rapMax;     // 規定ラップ数 最終的にはコースごとに持たせたい
    private int _playerNum;  // プレイヤー数

    public void SetUpRapCount(int rapMax)
    {
        _playerNum = GameObject.FindGameObjectsWithTag("RacingCar").Length;
        _rapCnt = new int[_playerNum];
        for (int idx = 0; idx < _playerNum; idx++)
        {
            _rapCnt[idx] = 1;
        }

        _rapMax = rapMax;

        rapText = rapText.GetComponent<TextMeshProUGUI>();
        // 現状、全プレイヤーの画面にプレイヤー１の周回数が反映されている
        // これを各プレイヤーごとにそれぞれの周回数表示に変更する必要がある
        // 画面分割、プレイヤーごとのカメラの設定などの作業中に関わってくると思われるため、
        // それまでは全プレイヤーにプレイヤー１の周回数を表示しておく
        rapText.text = _rapCnt[0] + " / " + _rapMax;
    }

    public void CountRap(int playerID)
    {
        _rapCnt[playerID]++;    // プレイヤーごとにラップカウントをとる
        if (_rapCnt[playerID] <= _rapMax)
        {
            rapText.text = (_rapCnt[playerID]) + " / " + _rapMax;
        }
        Debug.Log(rapText.text);
    }

    public bool CheckRapCount(int playerID)
    {
        return !(_rapCnt[playerID] <= _rapMax);
    }
}
