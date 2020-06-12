using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TimeSel : MonoBehaviour
{
    enum Stage { TimeAttackScene };
    GameObject _nowSelect;
    public List<Image> stages;
    [SerializeField] EventSystem eventSystem;
    GameObject _selectObj;
    List<string> _imageName;

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
                }
            }
        }
        if (!isCalledOnce)
        {
            ///ここを任意のボタンにしましょう。
            if (Input.GetKeyDown("space"))
            {
                isCalledOnce = true;
                FadeManager.Instance.LoadScene("TimeAttack02", 2.0f);
                Debug.Log("01へ");
            }
        }
    }
}
