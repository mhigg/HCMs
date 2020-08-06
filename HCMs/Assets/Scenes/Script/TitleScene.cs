using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    public DataStorage rankingStorage = null;

    void Start()
    {
        rankingStorage = rankingStorage.GetComponent<DataStorage>();
    }

    bool isCalledOnce = false;
    bool isDeleteOnce = false;
    bool isDeleteLapOnce = false;

    void Update()
    {
        if (!isCalledOnce)
        {
            if (Input.anyKey)
            {
            isCalledOnce = true;
                FadeManager.Instance.LoadScene("MenuScene", 2.0f);
                Debug.Log("Menuへ");
            }
        }

//------------------------------デバッグ用機能------------------------------------
        if (!isDeleteOnce)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                isDeleteOnce = true;
                rankingStorage.DeleteData("TimeAttack01");
                rankingStorage.DeleteData("TimeAttack02");
                rankingStorage.DeleteData("TimeAttack03");
                rankingStorage.DeleteData("TimeAttack04");
                rankingStorage.DeleteData("BattleScene_01");
                rankingStorage.DeleteData("BattleScene_02");
                rankingStorage.DeleteData("BattleScene_03");
                rankingStorage.DeleteData("BattleScene_04");
                rankingStorage.DeleteData("Battle");
                Debug.Log("全ランキングデリート");
            }
        }

        if (!isDeleteLapOnce)
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                isDeleteOnce = true;
                rankingStorage.DeleteData("TARap");
                rankingStorage.DeleteData("BTRap");
                rankingStorage.DeleteData("TALap");
                rankingStorage.DeleteData("BTLap");
                Debug.Log("全ラップランキングデリート");
            }
        }
//--------------------------------------------------------------------------------
    }
}
