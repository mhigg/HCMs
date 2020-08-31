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

    public CheckPointFlag cp = null;

    public CPType cpType;
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

        if (!_isThrough[playerID])
        {
            if (throughObject.tag == "RacingCar")
            {
                if(cpType == CPType.StartOtherRoot)
                {
                    Debug.Log("別ルートcp通過");
                    // orチェックポイントのナンバー+1を次のチェックポイントとする
                    int thisCPName = int.Parse(gameObject.name.Trim("or".ToCharArray()));
                    Debug.Log($"次チェックポイント：or{(thisCPName + 1)}");
                    Debug.Log(gameObject.name);
                    cpCount.CountCheckPoint($"or{(thisCPName + 1)}");
                }
                else if(cpType == CPType.OtherRoot)
                {
                    if (gameObject.name == cpCount.GetNextCPName())
                    {
                        Debug.Log("別ルートcp通過");
                        // orチェックポイントのナンバー+1を次のチェックポイントとする
                        int thisCPName = int.Parse(gameObject.name.Trim("or".ToCharArray()));
                        Debug.Log($"次チェックポイント：or{(thisCPName + 1)}");
                        Debug.Log(gameObject.name);
                        cpCount.CountCheckPoint($"or{(thisCPName + 1)}");
                    }
                }
                else
                {
                    Debug.Log("別ルート最終cp通過");
                    Debug.Log($"次チェックポイント：or{cp.name}");
                    Debug.Log(gameObject.name);
                    cpCount.CountCheckPoint(cp.name);
                }
                _isThrough[playerID] = true;

            }
        }
    }
}
