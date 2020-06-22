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
        Ranking(checkPointCount.GetNowThroughCheckPointNum());
        /*次チェックポイントまでの距離*/
        
        int[] ranking = Ranking();  // 総合した順位

        rankText01.text = ranking[0].ToString();
        rankText02.text = ranking[1].ToString();
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

        Ranking(rapCounts);
    }

    // 渡された要素で順位付けを行い、_rankingListに追加する
    private void Ranking(int[] counts)
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
                    retRanking[playerID]++;
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