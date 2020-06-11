using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 保存データの取得
public class DataStorage : MonoBehaviour
{
    // PlayerPrefsに保存したデータをretData配列に格納し返り値として返す
    // float型配列
    // @KEY string型：データ読み出しのキー
    // @DATA_MAX int型：データを保存する配列の最大サイズ
    // @defData float型：デフォルトで(初期値として)入れておく値
    public float[] GetData(string KEY, int DATA_MAX, float defData)
    {
        string data = PlayerPrefs.GetString(KEY);
        float[] retData = new float[DATA_MAX];
        if (data.Length > 0)
        {
            string[] tmpData = data.Split(","[0]);
            
            for (int idx = 0; idx < tmpData.Length && idx < DATA_MAX; idx++)
            {
                retData[idx] = float.Parse(tmpData[idx]);
            }
            Debug.Log("データ取得");
        }
        else
        {
            Debug.Log("データなし");

            // DeleteData呼び出し後など、
            // 何も値を入れないと返り値として渡す配列の中がすべて0になってしまうので、
            // 一番大きい数値を入れて初期化しておく
            for (int idx = 0; idx < DATA_MAX; idx++)
            {
                retData[idx] = defData;
            }
        }

        return retData;
    }

    // データの削除
    // @KEY string型：削除するデータのキー
    public void DeleteData(string KEY)
    {
        if(PlayerPrefs.HasKey(KEY))
        {
            string data = PlayerPrefs.GetString(KEY);
            if (data.Length > 0)
            {
                Debug.Log("データ削除");
                PlayerPrefs.DeleteKey(KEY);
            }
        }
        else
        {
            Debug.Log("キーが存在しません");
        }
    }

}
