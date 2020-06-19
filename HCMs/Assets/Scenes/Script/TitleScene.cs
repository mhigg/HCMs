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
    bool isDeleteRapOnce = false;

    void Update()
    {
        if (!isCalledOnce)
        {
            ///ここを任意のボタンにしましょう。
            if (Input.GetButtonDown("Decision"))
            {
                isCalledOnce = true;
                FadeManager.Instance.LoadScene("MenuScene", 2.0f);
                Debug.Log("Menuへ");
            }
        }

        if (!isCalledOnce)
        {
            ///ここを任意のボタンにしましょう。
            if (Input.GetKeyDown(KeyCode.Return))
            {
                isCalledOnce = true;
                FadeManager.Instance.LoadScene("NoGitScene", 2.0f);
                Debug.Log("BattleSceneの代替Sceneへ");
            }
        }

        if (!isDeleteOnce)
        {
            ///ここを任意のボタンにしましょう。
            if (Input.GetKeyDown(KeyCode.F1))
            {
                isDeleteOnce = true;
                rankingStorage.DeleteData("TimeAttack01");
                rankingStorage.DeleteData("TimeAttack02");
                rankingStorage.DeleteData("TimeAttack03");
                rankingStorage.DeleteData("BattleScene_01");
                rankingStorage.DeleteData("BattleScene_02");
                rankingStorage.DeleteData("BattleScene_03");
                Debug.Log("全ランキングデリート");
            }
        }

        if (!isDeleteRapOnce)
        {
            ///ここを任意のボタンにしましょう。
            if (Input.GetKeyDown(KeyCode.F2))
            {
                isDeleteOnce = true;
                rankingStorage.DeleteData("TARap");
                rankingStorage.DeleteData("BTRap");
                Debug.Log("全ラップランキングデリート");
            }
        }
    }
}
