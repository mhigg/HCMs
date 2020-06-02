using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalFlag : MonoBehaviour
{
    public Text goalText = null;
    public TimeCount timeCounter = null;
    public CheckPoint checkPoint = null;

    string playerID = "P1";     // プレイヤーごとに持っていて、プレイヤーから渡されるのが理想(タイムアタックは１つだからその限りではないが)
    int rapCnt;                 // ラップカウント
    int checkPointCnt;          // チェックポイント通過カウント
    bool FinishCall = false;    // FinishCountテスト用

    const int checkPointCntMax = 16;

    // Start is called before the first frame update
    void Start()
    {
        goalText = goalText.GetComponent<Text>();
        goalText.gameObject.SetActive(false);

        timeCounter = timeCounter.GetComponent<TimeCount>();

        checkPoint = checkPoint.GetComponent<CheckPoint>();

        rapCnt = 1;
        checkPointCnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckPointCount(string playerID)
    {
        checkPointCnt++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!FinishCall)
        {
            if (other.gameObject.tag == "RacingCar")
            {
                if(checkPointCnt == checkPointCntMax/*チェックポイントをすべて通過していたら*/)
                {
                    Debug.Log("ラップタイム" + rapCnt.ToString());
                    rapCnt++;    // プレイヤーごとにラップカウントをとる
                    timeCounter.RapCount(playerID);
                    checkPointCnt = 0;
                }

                if (!(rapCnt <= 3))
                {
                    Debug.Log("ゴール");
                    goalText.text = "ＧＯＡＬ！！！";
                    goalText.gameObject.SetActive(true);
                    timeCounter.FinishCount(playerID);
                    FinishCall = true;
                }
            }
        }
    }
}
