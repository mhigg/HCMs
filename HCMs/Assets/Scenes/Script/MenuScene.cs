﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuScene : MonoBehaviour
{
    [SerializeField] EventSystem eventSystem;
    Button _timeAtt;
    Button _battle;
    Button _obst;
    GameObject _selectObj;
    // Start is called before the first frame update
    void Start()
    {
        _timeAtt = GameObject.Find("/Canvas/TimeAttack").GetComponent<Button>();
        _battle = GameObject.Find("/Canvas/Battle").GetComponent<Button>();
        _obst = GameObject.Find("/Canvas/Obs").GetComponent<Button>();
    }
    bool isCalledOnce = false;

    // Update is called once per frame
    void Update()
    {
        if (!isCalledOnce)
        {
            ///ここを任意のボタンにしましょう。
            ///タイムアタック
            if (Input.GetKeyDown("space"))
            {
                _selectObj = eventSystem.currentSelectedGameObject.gameObject;
                if (_selectObj.name == _timeAtt.name)
                {
                    FadeManager.Instance.LoadScene("TimeSelectScene", 2.0f);
                    Debug.Log("TimeSelectScene");
                }
                if (_selectObj.name == _battle.name)
                {
                    FadeManager.Instance.LoadScene("BattleSelectScene", 2.0f);
                    Debug.Log("BattleSelectScene");
                }
                if (_selectObj.name == _obst.name)
                {
                    FadeManager.Instance.LoadScene("ObstacleSelectScene", 2.0f);
                    Debug.Log("ObstacleSelectScene");
                }
                isCalledOnce = true;
            }
        }
    }
}
