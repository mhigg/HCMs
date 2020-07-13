using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointFlag : MonoBehaviour
{
    private int _playerNum;             // プレイヤー人数
    private bool[] _isThrough;          // このチェックポイントを通過したかのフラグを保存
    private List<string> _playerName;   // プレイヤー名を保存

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] racingCars = GameObject.FindGameObjectsWithTag("RacingCar");
        _playerNum = racingCars.Length;
        _isThrough = new bool[_playerNum];
        for (int idx = 0; idx < _playerNum; idx++)
        {
            _isThrough[idx] = false;
        }

        _playerName = new List<string>();
        for (int idx = 0; idx < racingCars.Length; idx++)
        {
            // 全プレイヤー分のプレイヤー名を保存
            _playerName.Add(racingCars[idx].transform.parent.name);
        }
    }

    public void ResetThroughFlag(int playerID)
    {
        _isThrough[playerID] = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject throughObject = other.gameObject;

        int playerID = 0;
        for (int idx = 0; idx < _playerNum; idx++)
        {
            if (throughObject.transform.parent.name == _playerName[idx])
            {
                // プレイヤー名を照合して、playerIDに変換
                playerID = idx;
                Debug.Log("プレイヤー" + playerID + "チェックポイント通過");
            }
            else
            {
                Debug.LogError("プレイヤー名が未登録です");
            }
        }

        int checkPointCnt = throughObject.GetComponent<CheckPointCount>().GetNowThroughCheckPointNum();

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
