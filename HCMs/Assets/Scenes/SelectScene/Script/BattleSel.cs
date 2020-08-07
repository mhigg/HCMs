using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleSel : MonoBehaviour
{
    GameObject _nowSelect;
    public List<Image> stages;
    public List<int> _num;
    [SerializeField] EventSystem eventSystem;
    GameObject _selectObj;
    int _nowSelected = 0;
    List<string> _imageName;
    public List<bool> _stageAble;
    public Image _disableImg;

    void Start()
    {
        var tmp = _stageAble.Count;
        _imageName = new List<string>(tmp);
        for (int i = 0; i < tmp; i++)
        {
            _imageName.Add($"{i}");
        }
    }

    bool isCalledOnce = false;

    // Update is called once per frame
    void Update()
    {
        if (eventSystem.currentSelectedGameObject.gameObject == _selectObj)
        {
        }
        else
        {
            _selectObj = eventSystem.currentSelectedGameObject.gameObject;
            for (int i = 0; i < stages.Count; i++)
            {
                Vector3 pos = stages[i].transform.localPosition;
                pos.x = -5000;
                stages[i].transform.localPosition = pos;
            }
            for (int i = 0; i < stages.Count; i++)
            {
                if (_selectObj.name == _imageName[i])
                {
                    Vector3 pos = stages[i].transform.localPosition;
                    pos.x = 0;
                    stages[i].transform.localPosition = pos;
                    pos.x = 1200;
                    if (i + 1 < stages.Count)
                    {
                        stages[i + 1].transform.localPosition = pos;
                    }
                    pos.x = -1200;
                    if (i - 1 >= 0)
                    {
                        stages[i - 1].transform.localPosition = pos;
                    }
                    _nowSelected = i;
                }
            }
        }
        if (_stageAble[_nowSelected])
        {
            _disableImg.gameObject.SetActive(false);
            if (!isCalledOnce)
            {
                ///ここを任意のボタンにしましょう。
                if (Input.GetButtonDown("Decision"))
                {
                    isCalledOnce = true;
                    FadeManager.Instance.LoadScene("BattleScene_0" + $"{_num[_nowSelected]}", 2.0f);
                    Debug.Log("BattleSceneへ");
                }
            }
        }
        else
        {
            _disableImg.gameObject.SetActive(true);
        }
    }
}
