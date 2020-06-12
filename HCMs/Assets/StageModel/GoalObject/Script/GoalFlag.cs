using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalFlag : MonoBehaviour
{
    public Text goalText = null;
    public TimeCount timeCounter = null;
    public RapCount rapCount = null;
    public CheckPointCount checkPointCount = null;
    
    private bool[] FinishCall;    // FinishCountテスト用
    private int _playerNum;

    // Start is called before the first frame update
    void Start()
    {
        goalText = goalText.GetComponent<Text>();
        goalText.gameObject.SetActive(false);

        timeCounter = timeCounter.GetComponent<TimeCount>();
        rapCount = rapCount.GetComponent<RapCount>();

        checkPointCount = checkPointCount.GetComponent<CheckPointCount>();
    }

    public void SetUpGoalFlag(int playerNum, int rapMax)
    {
        _playerNum = playerNum;
        FinishCall = new bool[_playerNum];
        for (int playerID = 0; playerID < _playerNum; playerID++)
        {
            FinishCall[playerID] = false;
        }

        rapCount.SetUpRapCount(_playerNum, rapMax);
        checkPointCount.SetUpCheckPointCount(_playerNum);
    }

    // ゴール可能かどうかの判定を返す
    public bool CheckGoal(int playerID)
    {
        return FinishCall[playerID];
    }

    private void OnTriggerEnter(Collider other)
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

        if (!FinishCall[playerID])
        {
            if (other.gameObject.tag == "RacingCar")
            {
                if(checkPointCount.JudgThroughGoalSpace(playerID))
                {
                    rapCount.CountRap(playerID);
                    timeCounter.RapCount(playerID);
                }

                if (rapCount.CheckRapCount(playerID))
                {
                    Debug.Log("ゴール");
                    goalText.text = "ＦＩＮＩＳＨ！";
                    goalText.gameObject.SetActive(true);
                    timeCounter.FinishCount(playerID);
                    FinishCall[playerID] = true;
                }
            }
        }
    }
}
