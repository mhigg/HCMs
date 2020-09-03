using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointFlag : MonoBehaviour
{
    private bool[] _isThrough;          // このチェックポイントを通過したかのフラグを保存

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] racingCars = GameObject.FindGameObjectsWithTag("RacingCar");
        int _playerNum = racingCars.Length;
        _isThrough = new bool[_playerNum];
        for (int idx = 0; idx < _playerNum; idx++)
        {
            _isThrough[idx] = false;
        }
    }

    public void ResetThroughFlag(int playerID)
    {
        _isThrough[playerID] = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject throughObject = other.gameObject;

        int playerID = FindInfoByScene.Instance.GetPlayerID(throughObject.transform.parent.name);
        Debug.Log("プレイヤー" + playerID + "チェックポイント通過");

        CheckPointCount cpCount = throughObject.GetComponent<CheckPointCount>();

        int checkPointCnt = cpCount.GetNowThroughCheckPointNum();
        Debug.Log("第" + (checkPointCnt + 1) + "チェックポイント");
        Debug.Log("通過数：" + checkPointCnt);

        if (!_isThrough[playerID])
        {
            if (throughObject.tag == "RacingCar")
            {
                if (gameObject.name == cpCount.GetNextCPName())
                {
                    // ゴール通過すると_checkPointCntが0に戻るのでここで特にmax時のif処理を書く必要はない
                    Debug.Log("次チェックポイント：" + $"cp{(checkPointCnt + 2)}");
                    Debug.Log(gameObject.name);
                    cpCount.CountCheckPoint();
                    cpCount.LastThroughCheckPoint(this.name);
                    _isThrough[playerID] = true;
                }
            }
        }
    }
}
