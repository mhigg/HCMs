using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TimeSel : MonoBehaviour
{
    enum Stage { TimeAttackScene };
    int _nowSelected;
    public List<Image> _stages;
    public List<bool> _num;
    [SerializeField] EventSystem eventSystem;
    GameObject _selectObj;
    List<string> _imageName;
    public Image _disableImg;

    void Start()
    {
        var tmp = _stages.Count;
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
            for (int i = 0; i < _stages.Count; i++)
            {
                if (_selectObj.name == _imageName[i])
                {
                    Vector3 pos = _stages[i].transform.localPosition;
                    pos.x = 0;
                    _stages[i].transform.localPosition = pos;
                    pos.x = 1200;
                    _stages[(i + 1) % _stages.Count].transform.localPosition = pos;
                    pos.x = -1200;
                    _stages[(i + _stages.Count - 1) % _stages.Count].transform.localPosition = pos;
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
                    FadeManager.Instance.LoadScene("TimeAttack0" + $"{_nowSelected + 1}", 2.0f);
                    Debug.Log("01へ");

                }
            }
        }

        else
        {
            _disableImg.gameObject.SetActive(true);
        }
    }
}
