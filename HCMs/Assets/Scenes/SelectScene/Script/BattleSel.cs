using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleSel : MonoBehaviour
{
    GameObject _nowSelect;
    public List<Image> stages;
    [SerializeField] EventSystem eventSystem;
    GameObject _selectObj;
    int _nowSelected = 0;
    List<string> _imageName;
    public List<bool> _num;
    public Image _disableImg;

    void Start()
    {
        var tmp = stages.Count;
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
                if (_selectObj.name == _imageName[i])
                {
                    Vector3 pos = stages[i].transform.localPosition;
                    pos.x = 0;
                    stages[i].transform.localPosition = pos;
                    pos.x = 1200;
                    stages[(i + 1) % stages.Count].transform.localPosition = pos;
                    pos.x = -1200;
                    stages[(i + stages.Count - 1) % stages.Count].transform.localPosition = pos;
                    _nowSelected = i;
                }
            }
        }
        if (_num[_nowSelected])
        {
            _disableImg.gameObject.SetActive(false);
            if (!isCalledOnce)
            {
                ///ここを任意のボタンにしましょう。
                if (Input.GetButtonDown("Decision"))
                {
                    isCalledOnce = true;
                    FadeManager.Instance.LoadScene("BattleScene_0" + $"{_nowSelected + 1}", 2.0f);
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
