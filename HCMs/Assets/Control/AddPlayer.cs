using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlayer : MonoBehaviour
{
    int _padNum;                // パッドの数
    GameObject _childObject1;    // 子のオブジェクト
    GameObject _childObject2;    // 子のオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        string[] cName = Input.GetJoystickNames();
        _padNum = 0;
        for (int i = 0; i < cName.Length; i++)
        {
            if (cName[i] != "")
            {
                _padNum++;
            }
        }
        _childObject1 = transform.GetChild(0).gameObject;   // player2
        _childObject2 = transform.GetChild(1).gameObject;   // cpu
        if (_padNum <= 1)
        {
            _childObject1.SetActive(false);
            _childObject2.SetActive(true);
        }
        if (_padNum >= 2)       
        {
            _childObject1.SetActive(true);
            _childObject2.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
