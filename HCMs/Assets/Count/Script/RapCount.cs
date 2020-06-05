using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RapCount : MonoBehaviour
{
    public TextMeshProUGUI rapText;

    int[] rapCnt;   // ラップカウント
    int rapMax;     // 規定ラップ数 最終的にはコースごとに持たせたい
    int playerNum;  // プレイヤー数

    // Start is called before the first frame update
    void Start()
    {
        playerNum = 1;
        rapCnt = new int[playerNum];
        for(int idx = 0; idx < playerNum; idx++)
        {
            rapCnt[idx] = 1;
        }
        rapMax = 3;

        rapText = rapText.GetComponent<TextMeshProUGUI>();
        rapText.text = rapCnt[0] + " / " + rapMax;
    }

    public void CountRap(int playerID)
    {
        rapCnt[playerID]++;    // プレイヤーごとにラップカウントをとる
        if(rapCnt[playerID] <= rapMax)
        {
            rapText.text = (rapCnt[playerID]) + " / " + rapMax;
        }
        Debug.Log(rapText.text);
    }

    public bool CheckRapCount(int playerID)
    {
        return !(rapCnt[playerID] <= rapMax);
    }
}
