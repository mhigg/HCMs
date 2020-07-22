using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBestTime : MonoBehaviour
{
    private Dictionary<string, float> _bestTimes;
    private string _bestTimeStr;

    // Start is called before the first frame update
    void Start()
    {
        _bestTimes = new Dictionary<string, float>();
        _bestTimeStr = JsonUtility.ToJson(_bestTimes);
    }

    public void CompareBestTime(string playerID, float goalTime)
    {
        _bestTimes = JsonUtility.FromJson<Dictionary<string, float>>(_bestTimeStr);

        if (ExistsBestTime(_bestTimes, playerID))
        {
            // すでに自己ベスト記録があるなら比較
            if(_bestTimes[playerID] < goalTime)
            {
                Debug.Log("自己ベスト更新");
                _bestTimes[playerID] = goalTime;
            }
            else
            {
                Debug.Log("自己ベスト更新ならず");
            }
        }
        else
        {
            // 自己ベスト記録が無いならそのまま自己ベストに登録
            Debug.Log("自己ベストを新しく記録");
            _bestTimes[playerID] = goalTime;
        }

        _bestTimeStr = JsonUtility.ToJson(_bestTimes);
    }

    // プレイヤー名またはIDで検索し、自己ベストがあるかを返す
    // コース名でも検索
    private bool ExistsBestTime(Dictionary<string, float> bestTimes, string playerID)
    {
        if(bestTimes.ContainsKey(playerID))
        {
            Debug.Log($"{playerID}P自己ベストあり");
            return true;
        }

        Debug.Log($"{playerID}P自己ベストなし");
        return false;
    }
}
