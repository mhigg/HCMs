﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindInfoByScene : MonoBehaviour
{
    // 最大周回数テーブル
    private Dictionary<string, int> _stageLapMax;

    // プレイヤー名を保存
    private List<string> _playerName = new List<string>();
    
    // エントリーしているプレイヤー数
    private int _playerNum;

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

        // ステージ名テーブル
        List<string> _stageNameTbl = new List<string>();

        CSVReader csvReader = GetComponent<CSVReader>();
        var csvFile = csvReader.LoadCSV("stages");
        foreach(string[] name in csvFile)
        {
            for(int idx = 0; idx < name.Length; idx++)
            {
                Debug.Log("stageName:" + name[idx]);
                _stageNameTbl.Add(name[idx]);
            }
        }

        _stageLapMax = new Dictionary<string, int>();
        foreach (string stageName in _stageNameTbl)
        {
            _stageLapMax.Add(stageName, 3);
        }

        //if(GameObject.Find("Player2") == null)
        //{
        //    // プレイヤー２が複数無いならそのままエントリーする
        //    EntryPlayerName();
        //}
    }

    // アクティブ状態のプレイヤーをエントリーする(名前を記録する)
    public void EntryPlayerName()
    {
        Debug.Log("EntryPlayerName");
        if(_playerName.Count > 0)
        {
            _playerName.Clear();
        }
        GameObject[] racingCars = GameObject.FindGameObjectsWithTag("RacingCar");
        for (int idx = 0; idx < racingCars.Length; idx++)
        {
            if (racingCars[idx].transform.parent.gameObject.activeSelf)
            {
                // 全プレイヤー分のプレイヤー名を保存
                Debug.Log("登録：" + racingCars[idx].transform.parent.name);
                _playerName.Add(racingCars[idx].transform.parent.name);
            }
        }

        _playerNum = _playerName.Count;
    }

    public int GetPlayerNum()
    {
        return _playerNum;
    }

    public int GetLapMax(string activeStageName)
    {
        int retLapMax = _stageLapMax[activeStageName];

        if (retLapMax <= 0)
        {
            Debug.LogError("最大周回数が0以下です。コース情報の照合に失敗した可能性があります。_stageNameTblと_lapMaxを確認してください。");
        }

        return retLapMax;
    }

    public int GetPlayerID(string throughPlayerName)
    {
        int retID = -1;
        for (int idx = 0; idx < _playerName.Count; idx++)
        {
            if (throughPlayerName == _playerName[idx])
            {
                // プレイヤー名を照合して、playerIDに変換
                retID = idx;
                //Debug.Log("ID:" + retID);
            }
        }

        if(retID == -1)
        {
            Debug.LogError("プレイヤー名が未登録です");
        }

        return retID;
    }

    public string GetPlayerName(int playerNo)
    {
        string retName = "noPlayer";
        if (playerNo < _playerName.Count)
        {
            // プレイヤー名を取得
            retName = _playerName[playerNo];
        }

        if (retName == "noPlayer")
        {
            Debug.LogError("プレイヤーIDが未登録です");
        }

        return retName;
    }
}
