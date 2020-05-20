using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstSel : MonoBehaviour
{
    void Start()
    {

    }

    bool isCalledOnce = false;

    void Update()
    {
        if (!isCalledOnce)
        {
            ///ここを任意のボタンにしましょう。
            if (Input.GetKeyDown("space"))
            {
                isCalledOnce = true;
                FadeManager.Instance.LoadScene("ObstacleScene", 2.0f);
                Debug.Log("ObstacleScene");
            }
        }
    }
}
