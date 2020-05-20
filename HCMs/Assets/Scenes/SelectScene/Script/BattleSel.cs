using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSel : MonoBehaviour
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
                FadeManager.Instance.LoadScene("BattleScene", 2.0f);
                Debug.Log("Batlleへ");
            }
        }
    }
}
