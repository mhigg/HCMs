using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScene : MonoBehaviour
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
            ///クリアしたらリザルトへ
            if (Input.GetKeyDown("space"))
            {
                isCalledOnce = true;
                FadeManager.Instance.LoadScene("ObstacleResult", 2.0f);
                Debug.Log("Resultへ");
            }
            ///ゲームオーバーならゲームオーバー画面へ
            if (Input.GetKeyDown("enter"))
            {
                isCalledOnce = true;
                FadeManager.Instance.LoadScene("GameOverScene", 2.0f);
                Debug.Log("GameOverへ");
            }
        }
    }
}
