﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalFlag : MonoBehaviour
{
    public Text goalText = null;
    public TimeCount timeCounter = null;
    public CheckPointCount checkPointCount = null;

    private bool[] _finishCall;     // FinishCountテスト用
    private int _playerNum;         // プレイヤー人数

    // Start is called before the first frame update
    void Start()
    {
        goalText = goalText.GetComponent<Text>();
        goalText.gameObject.SetActive(false);

        timeCounter = timeCounter.GetComponent<TimeCount>();

        checkPointCount = checkPointCount.GetComponent<CheckPointCount>();

        _playerNum = GameObject.FindGameObjectsWithTag("RacingCar").Length;
        _finishCall = new bool[_playerNum];
        for (int playerID = 0; playerID < _playerNum; playerID++)
        {
            _finishCall[playerID] = false;
        }
    }

    // ゴール可能かどうかの判定を返す
    public bool CheckFinish()
    {
        bool retFinish = true;
        for(int playerID = 0; playerID < _playerNum; playerID++)
        {
            // 一つでもfalseがあるとfalseになり、まだゴールしていないプレイヤーがいると判断
            retFinish &= _finishCall[playerID];
        }
        return retFinish;
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
         other.gameObjectのプレイヤー名を照会してその添字をplayerIDとする
         ex)
         for(int idx = 0; idx < playerName.Length; idx++)
         {
            if(other.gameObject.name == playerName[idx])
            {
                playerID = idx;
            }
         }
         つまり0～3となる
         現状は0(1プレイヤー目)とする
         */

        GameObject throughObject = other.gameObject;

        // 仮に車のbodyの名前を0と1にして直接playerIDとして扱う
        int playerID = int.Parse(throughObject.name);
        Debug.Log("プレイヤー" + playerID + "ゴール通過");

        if (!_finishCall[playerID])
        {
            if (throughObject.tag == "RacingCar")
            {
                if (checkPointCount.JudgThroughGoalSpace(playerID))
                {
                    throughObject.GetComponent<RapCount>().CountRap();
                    timeCounter.RapCount(playerID);
                }

                if (throughObject.GetComponent<RapCount>().CheckRapCount())
                {
                    Debug.Log("プレイヤー" + playerID + "ゴール");
                    goalText.text = "ＦＩＮＩＳＨ！";
                    goalText.gameObject.SetActive(true);
                    timeCounter.FinishCount(playerID);
                    _finishCall[playerID] = true;
                }
            }
        }
    }
}
