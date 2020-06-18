using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointFlag : MonoBehaviour
{
    public CheckPointCount checkPointCount = null;

    private int _checkPointCnt;     // チェックポイント通過数カウント(プレイヤー分必要？)
    private int _playerNum;         // プレイヤー人数
    private bool[] _isThrough;      // このチェックポイントを通過したかのフラグを保存

    // Start is called before the first frame update
    void Start()
    {
        checkPointCount = checkPointCount.GetComponent<CheckPointCount>();

        _playerNum = GameObject.FindGameObjectsWithTag("RacingCar").Length;
        _isThrough = new bool[_playerNum];
        for (int idx = 0; idx < _playerNum; idx++)
        {
            Debug.Log("_isThrough " + idx + "プレイヤー初期化");
            _isThrough[idx] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int playerID = 0; playerID < _playerNum; playerID++)
        {
            _checkPointCnt = checkPointCount.GetNowThroughCheckPointNum(playerID);
            if (_isThrough[playerID] && (_checkPointCnt <= 0))
            {
                Debug.Log("全チェックポイント通過");
                Debug.Log("全チェックポイントを未通過状態にする");
                _isThrough[playerID] = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
         other.gameObjectのプレイヤー名を照会してその添字をplayerIDとする
         つまり0～3となる
         現状は0(1プレイヤー目)とする
         */

        // 現状プレイヤー名の登録は実装していないため、車のbodyの名前を0と1にして直接playerIDとして扱う
        int playerID = int.Parse(other.gameObject.name);
        Debug.Log("プレイヤー" + playerID + "チェックポイント通過");

        if (!_isThrough[playerID])
        {
            if (other.gameObject.tag == "RacingCar")
            {
                Debug.Log("第" + (_checkPointCnt + 1) + "チェックポイント通過");
                Debug.Log(this.gameObject.name);
                checkPointCount.CountCheckPoint(playerID);
                _isThrough[playerID] = true;
            }
        }
    }
}
