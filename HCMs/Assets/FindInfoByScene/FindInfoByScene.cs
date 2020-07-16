using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindInfoByScene : MonoBehaviour
{
    // ステージ名テーブル
    private string[] _stageNameTbl;

    // 最大周回数テーブル
    private Dictionary<string, int> _stageRapMax;

    // プレイヤー名を保存
    private List<string> _playerName;

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
            "BattleScene_01",       "BattleScene_02",       "BattleScene_03",       "BattleScene_04",
            "BattleResult01",       "BattleResult02",       "BattleResult03",       "BattleResult04",
            "TimeAttack01",         "TimeAttack02",         "TimeAttack03",         "TimeAttack04",
            "TimeAttackResult01",   "TimeAttackResult02",   "TimeAttackResult03",   "TimeAttackResult04"
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
        GameObject[] racingCars = GameObject.FindGameObjectsWithTag("RacingCar");
        _playerName = new List<string>();
        for (int idx = 0; idx < racingCars.Length; idx++)
        {
            // 全プレイヤー分のプレイヤー名を保存
            Debug.Log("登録：" + racingCars[idx].transform.parent.name);
            _playerName.Add(racingCars[idx].transform.parent.name);
        }
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

    public int GetPlayerID(string throughPlayerName)
    {
        Debug.Log("通過名：" + throughPlayerName);
        int retID = -1;
        for (int idx = 0; idx < _playerName.Count; idx++)
        {
            if (throughPlayerName == _playerName[idx])
            {
                // プレイヤー名を照合して、playerIDに変換
                retID = idx;
                Debug.Log("ID:" + retID);
            }
        }

        if(retID == -1)
        {
            Debug.LogError("プレイヤー名が未登録です");
        }

        return retID;
    }
}
