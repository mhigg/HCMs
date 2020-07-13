using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindInfoByScene : MonoBehaviour
{
    // ステージ名テーブル
    private string[] _stageNameTbl;

    // 最大周回数テーブル
    private Dictionary<string, int> _stageRapMax;

    #region Singleton

    private static FindInfoByScene instance;

    public static FindInfoByScene Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (FindInfoByScene)FindObjectOfType(typeof(FindInfoByScene));

                if (instance == null)
                {
                    Debug.LogError(typeof(FindInfoByScene) + "is nothing");
                }
            }

            return instance;
        }
    }

    #endregion Singleton

    public void Awake()
    {
        Debug.Log("FindInfoBySceneAwake");
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        _stageNameTbl = new string[]{
            "BattleScene_01",       "BattleScene_02",       "BattleScene_03",
            "BattleResult01",       "BattleResult02",       "BattleResult03",
            "TimeAttack01",         "TimeAttack02",         "TimeAttack03",
            "TimeAttackResult01",   "TimeAttackResult02",   "TimeAttackResult03"
        };

        _stageRapMax = new Dictionary<string, int>();
        foreach (string stageName in _stageNameTbl)
        {
            _stageRapMax.Add(stageName, 3);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("FindInfoBySceneStart");
    }

    // Update is called once per frame
    public int GetRapMax(string activeStageName)
    {
        int retRapMax = _stageRapMax[activeStageName];

        if (retRapMax <= 0)
        {
            Debug.LogError("最大周回数が0以下です。コース情報の照合に失敗した可能性があります。_stageNameTblと_rapMaxを確認してください。");
        }

        return retRapMax;
    }
}
