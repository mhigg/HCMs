using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 保存データの取得
public class DataStorage : MonoBehaviour
{
    // PlayerPrefsにデータを格納する(floatデータ限定)
    // @KEY string型：データ格納のキー
    // @saveData float型配列：格納するデータ配列
    public void SaveData(string KEY, float[] saveData)
    {
        if(saveData.Length > 0)
        {
            string data_string = string.Join(",", saveData);
            PlayerPrefs.SetString(KEY, data_string);
        }
        else
        {
            Debug.LogError("セーブするデータが空です:DataStorage.SaveData");
        }
    }

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
            Debug.Log("データなし:デフォルト値にて初期化します");

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
            Debug.Log("削除不可:キーが存在しません");
        }
    }

}
