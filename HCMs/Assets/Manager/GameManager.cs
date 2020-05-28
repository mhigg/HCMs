using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 保留
public class GameManager : MonoBehaviour
{
    private string _gameMode;    // ゲームモード
    private int _playNum = 4;    // プレイ人数
    private int _rapMax = 3;     // ラップ(周回)数

    #region Singleton

    private static GameManager s_instance;
    
    public static GameManager Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = (GameManager)FindObjectOfType(typeof(GameManager));

                if (s_instance == null)
                {
                    Debug.LogError(typeof(GameManager) + "is nothing");
                }
            }

            return s_instance;
        }
    }

    #endregion Singleton

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public string GetRankingKey()
    {
        string key;

        switch (_gameMode)
        {
            case "timeAttack":
                key = "timeAttack";
                break;
            case "battle":
                key = "battle";
                break;
            case "obstacle":
                key = "obstacle";
                break;
            default:
                key = "timeAttack";
                break;
        }

        return key;
    }

    public void SetGameMode(string gameMode)
    {
        _gameMode = gameMode;
    }

    public string GetGameMode()
    {
        return _gameMode;
    }

    public void SetPlayNum(int playNum)
    {
        _playNum = playNum;
    }

    public int GetPlayNum()
    {
        return _playNum;
    }

    public void SetRapMax(int rapMax)
    {
        _rapMax = rapMax;
    }

    public int GetRapMax()
    {
        return _rapMax;
    }

}
