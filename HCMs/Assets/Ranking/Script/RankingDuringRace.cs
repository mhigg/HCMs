﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 周回数やチェックポイント通過数の比較結果から順位表示を更新する
public class RankingDuringRace : MonoBehaviour
{
    public CheckPointCount checkPointCount;
    public TextMeshProUGUI rankText01;
    public TextMeshProUGUI rankText02;

    private int _playerNum;             // プレイヤー人数
    private List<int[]> _rankingList;   // 順位リスト

    // Start is called before the first frame update
    void Start()
    {
        checkPointCount = checkPointCount.GetComponent<CheckPointCount>();
        rankText01 = rankText01.GetComponent<TextMeshProUGUI>();
        rankText02 = rankText02.GetComponent<TextMeshProUGUI>();
        _playerNum = GameObject.FindGameObjectsWithTag("RacingCar").Length;
        _rankingList = new List<int[]> (_playerNum);
    }

    // Update is called once per frame
    void Update()
    {
        // 周回数→チェックポイント通過数→次チェックポイントまでの距離の順にランク付けする
        CompareRapCountAndGetRanking();
        Ranking(checkPointCount.GetNowThroughCheckPointNum(), false);
        CompareDistanceToNextPoint();

        int[] ranking = Ranking();  // 総合した順位

        rankText01.text = RankingToString(ranking[0]);
        rankText02.text = RankingToString(ranking[1]);
    }

    private string RankingToString(int ranking)
    {
        string retString;
        switch(ranking)
        {
            case 1:
                retString = ranking.ToString() + "st";
                break;
            case 2:
                retString = ranking.ToString() + "nd";
                break;
            case 3:
                retString = ranking.ToString() + "rd";
                break;
            default:
                retString = ranking.ToString() + "th";
                break;
        }

        return retString;
    }

    // 周回数の順位付け
    private void CompareRapCountAndGetRanking()
    {
        GameObject[] racingCars = GameObject.FindGameObjectsWithTag("RacingCar");
        int[] rapCounts = new int[racingCars.Length];
        foreach (GameObject player in racingCars)
        {
            int id = int.Parse(player.name);
            rapCounts[id] = player.GetComponentInChildren<RapCount>().GetRapCount();
            Debug.Log("プレイヤー:" + id + " 周回数:" + rapCounts[id]);
        }

        Ranking(rapCounts, false);
    }

    //次チェックポイントまでの距離で順位付け
    private void CompareDistanceToNextPoint()
    {
        GameObject[] racingCars = GameObject.FindGameObjectsWithTag("RacingCar");
        int[] throughCpNums = checkPointCount.GetNowThroughCheckPointNum();
        GameObject[] checkPoints = GameObject.FindGameObjectsWithTag("CheckPoint");

        int[] distanceBlock = new int[throughCpNums.Length];
        for(int playerID = 0; playerID < throughCpNums.Length; playerID++)
        {
            for (int idx = 0; idx < (checkPoints.Length - 1); idx++)
            {
                // 通過数に１足した値を次のチェックポイントとする
                if (checkPoints[idx].name == ("cp" + (throughCpNums[playerID] + 1).ToString()))
                {
                    // 次のチェックポイントまでの距離
                    float distance = Vector3.Distance(checkPoints[idx].transform.position, racingCars[playerID].transform.position);
                    Debug.Log(playerID + "P distance:" + distance);
                    // 小数点以下を整数値に上げる
                    distanceBlock[playerID] = Mathf.FloorToInt(distance * 1000);
                }
            }
        }

        Ranking(distanceBlock, true);
    }

    // 渡された要素で順位付けを行い、_rankingListに追加する
    // @conts int型配列：順位付けを行う要素
    // @isAsc bool型：昇順ならtrue、降順ならfalse
    private void Ranking(int[] counts, bool isAsc)
    {
        int[] retRanking = new int[_playerNum];

        for (int rank = 0; rank < _playerNum; rank++)
        {
            retRanking[rank] = 1;
        }

        for (int playerID = 0; playerID < _playerNum; playerID++)
        {
            for (int comparison = 0; comparison < _playerNum; comparison++)
            {
                if (isAsc)
                {
                    if (playerID != comparison && counts[comparison] < counts[playerID])
                    {
                        retRanking[playerID]++;
                    }
                }
                else
                {
                    if (playerID != comparison && counts[playerID] < counts[comparison])
                    {
                        retRanking[playerID]++;
                    }
                }
            }
        }

        _rankingList.Add(retRanking);
    }

    private int[] Ranking()
    {
        int[] retRanking = new int[_playerNum];

        for (int rank = 0; rank < _playerNum; rank++)
        {
            retRanking[rank] = 1;
        }

        int[][] rankingList = _rankingList.ToArray();
        for (int playerID = 0; playerID < _playerNum; playerID++)
        {
            for (int comparison = 0; comparison < _playerNum; comparison++)
            {
                // 順位比較
                retRanking = RankDown(retRanking, rankingList, playerID, comparison);
            }
        }

        Debug.Log("Total.1P" + retRanking[0]);
        Debug.Log("Total.2P" + retRanking[1]);
        _rankingList.Clear();   // 次の順位付けのためにクリア
        return retRanking;
    }

    private int _downCnt = 0;   // RankDownで使用。同順位だった際にカウントプラスする

    // _rankingListの内容をもとに全体的な順位の統合を行う
    // 周回数が同じならチェックポイント通過数、それも同じなら次チェックポイントまでの距離
    // といった順に順位を比較し、最終的な順位を決める
    private int[] RankDown(int[] retRanking, int[][] ranking, int playerID, int comparison)
    {
        if (ranking[_downCnt][comparison] < ranking[_downCnt][playerID])
        {
            retRanking[playerID]++;
        }
        else if (ranking[_downCnt][comparison] == ranking[_downCnt][playerID])
        {
            _downCnt++;
            if(_downCnt < _rankingList.Count)
            {
                retRanking = RankDown(retRanking, ranking, playerID, comparison);
            }
        }

        _downCnt = 0;   // 順位を確定したら次の順位付けのためにゼロクリア
        return retRanking;
    }
}