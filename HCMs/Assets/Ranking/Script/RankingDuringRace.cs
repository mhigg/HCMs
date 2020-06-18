using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        checkPointCount = checkPointCount.GetComponent<CheckPointCount>();
        rankText01 = rankText01.GetComponent<TextMeshProUGUI>();
        rankText02 = rankText02.GetComponent<TextMeshProUGUI>();
        _playerNum = GameObject.FindGameObjectsWithTag("RacingCar").Length;
    }

    // Update is called once per frame
    void Update()
    {
        int[] rapRanking = CompareRapCountAndGetRanking();
        int[] cpRanking = CompareCountAndGetRankOfCheckPointCount();

        int[] ranking = Ranking(rapRanking, cpRanking);

        rankText01.text = ranking[0].ToString();
        rankText02.text = ranking[1].ToString();
    }

    // この関数を読んだ時点での全プレイヤーのチェックポイント通過数を比較し、
    // チェックポイント通過数の一時的なランキングを返す
    // ランキング配列はプレイヤーID順に保存
    public int[] CompareCountAndGetRankOfCheckPointCount()
    {
        int[] retRanking = Ranking(checkPointCount.GetNowThroughCheckPointNum());
        Debug.Log("CP.1P" + retRanking[0]);
        Debug.Log("CP.2P" + retRanking[1]);
        return retRanking;
    }

    private int[] CompareRapCountAndGetRanking()
    {
        GameObject[] racingCars = GameObject.FindGameObjectsWithTag("RacingCar");
        int[] rapCounts = new int[racingCars.Length];
        foreach (GameObject player in racingCars)
        {
            int id = int.Parse(player.name);
            rapCounts[id] = player.GetComponentInChildren<RapCount>().GetRapCount();
            Debug.Log("プレイヤー:" + id + " 周回数:" + rapCounts[id]);
        }

        int[] retRanking = Ranking(rapCounts);
        Debug.Log("Rap.1P" + retRanking[0]);
        Debug.Log("Rap.2P" + retRanking[1]);
        return retRanking;
    }

    private int[] Ranking(int[] counts)
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
                if (playerID != comparison && counts[playerID] < counts[comparison])
                {
                    // playerIDがcomparisonよりも通過数が少なかったら順位を下げる
                    retRanking[playerID]++;
                }
            }
        }

        return retRanking;
    }

    private int[] Ranking(int[] rap, int[] cp)
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
                // 順位比較 数値が小さいほうが順位上
                if (rap[comparison] < rap[playerID])
                {
                    retRanking[playerID]++;
                }
                else if (rap[comparison] == rap[playerID])
                {
                    if (cp[comparison] < cp[playerID])
                    {
                        retRanking[playerID]++;
                    }
                    else if(cp[comparison] == cp[playerID])
                    {
                        // ここでさらに距離で比較する
                    }
                }
            }
        }

        Debug.Log("Total.1P" + retRanking[0]);
        Debug.Log("Total.2P" + retRanking[1]);
        return retRanking;
    }
}