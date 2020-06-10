using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalFlag : MonoBehaviour
{
    public Text goalText = null;
    public TimeCount timeCounter = null;
    public RapCount rapCount = null;
    
    private int checkPointCnt;          // チェックポイント通過カウント
    private int checkPointCntMax;       // チェックポイントの数
    private bool FinishCall = false;    // FinishCountテスト用


    // Start is called before the first frame update
    void Start()
    {
        goalText = goalText.GetComponent<Text>();
        goalText.gameObject.SetActive(false);

        timeCounter = timeCounter.GetComponent<TimeCount>();
        rapCount = rapCount.GetComponent<RapCount>();

        checkPointCnt = 0;

        // ゴールによってカウントするチェックポイントのタグを変える
        checkPointCntMax = GameObject.FindGameObjectsWithTag("CheckPoint").Length;
        Debug.Log("チェックポイント全" + checkPointCntMax + "個");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ゴールを通過するために必要なチェックポイント通過数をプレイヤーごとにカウントする
    public void CheckPointCount(int playerID)
    {
        // checkPointCnt[playerID]++;
        checkPointCnt++;
    }

    // プレイヤーごとに現在通過したチェックポイント数を返す
    public int GetNowCheckPointCount(int playerID)
    {
        // return checkPointCnt[playerID];
        return checkPointCnt;
    }

    // ゴール可能かどうかの判定を返す
    public bool CheckGoal()
    {
        // return FinishCall[playerID];
        return FinishCall;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!FinishCall)
        {
            if (other.gameObject.tag == "RacingCar")
            {
                /*
                 other.gameObjectのプレイヤー名を照会してその添字をplayerIDとする
                 ex)
                 for(int idx = 0; idx < playerName.Length; idx++)
                 {
                    if(other.gameObject.name == playerName[idx])
                    {
                        playerID = idx;
                    }
                 }
                 つまり0～3となる
                 現状は0(1プレイヤー目)とする
                 */
                int playerID = 0;

                if(checkPointCnt == checkPointCntMax/*チェックポイントをすべて通過していたら*/)
                {
                    rapCount.CountRap(playerID);
                    timeCounter.RapCount(playerID);
                    checkPointCnt = 0;
                }

                if (rapCount.CheckRapCount(playerID))
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
