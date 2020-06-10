﻿using System.Collections;
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
            if (Input.GetKeyDown("space"))
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
                rankingStorage.DeleteData("TimeAttack");
                rankingStorage.DeleteData("Battle");
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
