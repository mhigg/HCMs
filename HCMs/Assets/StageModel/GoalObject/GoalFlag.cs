using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoalFlag : MonoBehaviour
{
    public Text goalText = null;
    public TimeCount timeCounter = null;

    string playerID = "P1";     // プレイヤーごとに持っていて、プレイヤーから渡されるのが理想(タイムアタックは１つだからその限りではないが)
    int rapCnt;                 // ラップカウント
    int checkPointCnt;          // チェックポイント通過カウント
    int checkPointCntMax;       // チェックポイントの数
    bool FinishCall = false;    // FinishCountテスト用


    // Start is called before the first frame update
    void Start()
    {
        goalText = goalText.GetComponent<Text>();
        goalText.gameObject.SetActive(false);

        timeCounter = timeCounter.GetComponent<TimeCount>();

        rapCnt = 1;
        checkPointCnt = 0;
        checkPointCntMax = GameObject.FindGameObjectsWithTag("CheckPoint").Length;
        Debug.Log("チェックポイント全" + checkPointCntMax + "個");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckPointCount(string playerID)
    {
        checkPointCnt++;
    }

    public int GetNowCheckPointCount(string playerID)
    {
        return checkPointCnt;
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
