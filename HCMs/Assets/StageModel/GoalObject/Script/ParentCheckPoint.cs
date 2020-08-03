using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// チェックポイントの親オブジェクトに持たせるスクリプト
public class ParentCheckPoint : MonoBehaviour
{
    // 自分の子に設定しているすべてのチェックポイントを未通過状態にする
    public void ResetCheckPointIsThrough(int playerID)
    {
        Debug.Log("全チェックポイント通過");
        Debug.Log("全チェックポイントを未通過状態にする");
        Transform parent = gameObject.transform;
        for(int idx = 0; idx < parent.childCount; idx++)
        {
            string cpRoot = parent.GetChild(idx).name.Substring(0, 2);
            if (cpRoot == "cp")
            {
                parent.GetChild(idx).GetComponent<CheckPointFlag>().ResetThroughFlag(playerID);
            }
            else if(cpRoot == "or")
            {
                parent.GetChild(idx).GetComponent<OtherRootCheckPoint>().ResetThroughFlag(playerID);
            }
        }
    }
}
