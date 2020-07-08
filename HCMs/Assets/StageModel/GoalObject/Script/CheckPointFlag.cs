﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointFlag : MonoBehaviour
{
    private int _playerNum;         // プレイヤー人数
    private bool[] _isThrough;      // このチェックポイントを通過したかのフラグを保存

    // Start is called before the first frame update
    void Start()
    {
        _playerNum = GameObject.FindGameObjectsWithTag("RacingCar").Length;
        _isThrough = new bool[_playerNum];
        for (int idx = 0; idx < _playerNum; idx++)
        {
            Debug.Log("_isThrough " + idx + "プレイヤー初期化");
            _isThrough[idx] = false;
        }
    }

    public void ResetThroughFlag(int playerID)
    {
        _isThrough[playerID] = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
         other.gameObjectのプレイヤー名を照会してその添字をplayerIDとする
         つまり0～3となる
         現状は0(1プレイヤー目)とする
         */

        GameObject throughObject = other.gameObject;

        // 現状プレイヤー名の登録は実装していないため、車のbodyの名前を0と1にして直接playerIDとして扱う
        int playerID = int.Parse(throughObject.name);    // ※PLAYERNAME※
        int checkPointCnt = throughObject.GetComponent<CheckPointCount>().GetNowThroughCheckPointNum();

        Debug.Log("プレイヤー" + playerID + "チェックポイント通過");

        if (!_isThrough[playerID])
        {
            if (throughObject.tag == "RacingCar")
            {
                if (this.gameObject.name == $"cp{checkPointCnt + 1}")
                {
                    // ゴール通過すると_checkPointCntが0に戻るのでここで特にmax時のif処理を書く必要はない
                    Debug.Log("第" + (checkPointCnt + 1) + "チェックポイント通過");
                    Debug.Log(this.gameObject.name);
                    throughObject.GetComponent<CheckPointCount>().CountCheckPoint();
                    _isThrough[playerID] = true;
                }
            }
        }
    }
}
