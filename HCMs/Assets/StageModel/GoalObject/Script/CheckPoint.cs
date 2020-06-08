using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour
{
    public GoalFlag goalFlag = null;
    int checkPointCnt;
    bool isThrough;
    int playerNum;

    // Start is called before the first frame update
    void Start()
    {
        goalFlag = goalFlag.GetComponent<GoalFlag>();
        isThrough = false;
        playerNum = 1;
    }

    // Update is called once per frame
    void Update()
    {
        for (int playerID = 0; playerID < playerNum; playerID++)
        {
            checkPointCnt = goalFlag.GetNowCheckPointCount(playerID);
            if (isThrough && (checkPointCnt <= 0))
            {
                Debug.Log("全チェックポイント通過");
                Debug.Log("全チェックポイントを未通過状態にする");
                isThrough = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isThrough)
        {
            if (other.gameObject.tag == "RacingCar")
            {
                /*
                 other.gameObjectのプレイヤー名を照会してその添字をplayerIDとする
                 つまり0～3となる
                 現状は0(1プレイヤー目)とする
                 */
                int playerID = 0;

                Debug.Log("第" + (checkPointCnt + 1) + "チェックポイント通過");
                Debug.Log(this.gameObject.name);
                goalFlag.CheckPointCount(playerID);
                isThrough = true;
            }
        }
    }
}
