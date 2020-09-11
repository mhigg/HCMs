using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCPU : MonoBehaviour
{
    int _padNum;                // パッドの数

    // Start is called before the first frame update
    void Awake()
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
        if (_padNum <= 1)
        {
            this.gameObject.SetActive(true);
        }
        if (_padNum >= 2)
        {
            this.gameObject.SetActive(false);
        }

        FindInfoByScene.Instance.EntryPlayerName();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
