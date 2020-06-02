using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour
{
    public GoalFlag goalFlag = null;
    string playerID;     // プレイヤーごとに持っていて、プレイヤーから渡されるのが理想(タイムアタックは１つだからその限りではないが)
    int checkPointCnt;
    bool isThrough;

    // Start is called before the first frame update
    void Start()
    {
        goalFlag = goalFlag.GetComponent<GoalFlag>();
        playerID = "P1";
        isThrough = false;
    }

    // Update is called once per frame
    void Update()
    {
        checkPointCnt = goalFlag.GetNowCheckPointCount(playerID);
        if(isThrough && (checkPointCnt <= 0))
        {
            Debug.Log("全チェックポイント通過");
            Debug.Log("全チェックポイントを未通過状態にする");
            isThrough = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isThrough)
        {
            if (other.gameObject.tag == "RacingCar")
            {
                Debug.Log("第" + (checkPointCnt + 1) + "チェックポイント通過");
                goalFlag.CheckPointCount(playerID);
                isThrough = true;
            }
        }
    }
}
