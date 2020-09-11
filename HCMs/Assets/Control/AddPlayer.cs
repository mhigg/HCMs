using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlayer : MonoBehaviour
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
            this.gameObject.SetActive(false);
        }
        if (_padNum >= 2)       
        {
            this.gameObject.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
