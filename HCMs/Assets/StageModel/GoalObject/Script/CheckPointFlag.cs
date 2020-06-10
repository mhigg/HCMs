using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointFlag : MonoBehaviour
{
    public CheckPointCount checkPointCount = null;

    private int checkPointCnt;
    private int playerNum;
    private bool[] isThrough;

    // Start is called before the first frame update
    void Start()
    {
        checkPointCount = checkPointCount.GetComponent<CheckPointCount>();

        playerNum = 1;
        isThrough = new bool[playerNum];
        for (int idx = 0; idx < playerNum; idx++)
        {
            isThrough[idx] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int playerID = 0; playerID < playerNum; playerID++)
        {
            checkPointCnt = checkPointCount.GetNowThroughCheckPointNum(playerID);
            if (isThrough[playerID] && (checkPointCnt <= 0))
            {
                Debug.Log("全チェックポイント通過");
                Debug.Log("全チェックポイントを未通過状態にする");
                isThrough[playerID] = false;
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
        int playerID = 0;

        if (!isThrough[playerID])
        {
            if (other.gameObject.tag == "RacingCar")
            {
                Debug.Log("第" + (checkPointCnt + 1) + "チェックポイント通過");
                Debug.Log(this.gameObject.name);
                checkPointCount.CountCheckPoint(playerID);
                isThrough[playerID] = true;
            }
        }
    }
}
