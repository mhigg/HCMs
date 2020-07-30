using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherRootCheckPoint : MonoBehaviour
{
    public enum CPType
    {
        StartOtherRoot,
        OtherRoot,
        EndOtherRoot
    }

    public CPType cpType;
    private int _playerNum;             // プレイヤー人数
    private bool[] _isThrough;          // このチェックポイントを通過したかのフラグを保存

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

        if (!_isThrough[playerID])
        {
            if (throughObject.tag == "RacingCar")
            {
                if (gameObject.name == cpCount.GetNextCPName())
                {
                    if(cpType != CPType.EndOtherRoot)
                    {
                        // orチェックポイントのナンバー+1を次のチェックポイントとする
                        int thisCPName = int.Parse(gameObject.name.Trim("or".ToCharArray()));
                        cpCount.CountCheckPoint(gameObject.name + (thisCPName + 1));
                    }
                    else
                    {
                        cpCount.CountCheckPoint();
                    }
                    _isThrough[playerID] = true;
                }
            }
        }
    }
}
