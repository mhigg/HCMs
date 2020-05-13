using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool isCalledOnce = false;
    // Update is called once per frame
    void Update()
    {
        if (!isCalledOnce)
        {
            ///ここを任意のボタンにしましょう。
            if (Input.GetKeyDown("space"))
            {
                isCalledOnce = true;
                FadeManager.Instance.LoadScene("TimeAttackResult", 2.0f);
                Debug.Log("Resultへ");
            }
        }
    }
}
