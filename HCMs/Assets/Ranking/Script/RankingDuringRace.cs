using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 周回数やチェックポイント通過数の比較結果から順位表示を更新する
public class RankingDuringRace : MonoBehaviour
{
    public List<TextMeshProUGUI> rankTextList;  // 順位表示のリスト
    public List<ImageNo> imageNo;

    private int _playerNum;             // プレイヤー人数
    private List<int[]> _rankingList;   // 順位リスト

    // Start is called before the first frame update
    void Start()
    {
        _playerNum = FindInfoByScene.Instance.GetPlayerNum();
        _rankingList = new List<int[]>(_playerNum);

        for (int playerID = 0; playerID < _playerNum; playerID++)
        {
            // 順位表示の初期化
            rankTextList[playerID].text = RankingToString(playerID + 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 周回数→チェックポイント通過数→次チェックポイントまでの距離の順にランク付けする
        CompareLapCountAndGetRanking();
        CompareCheckPointCountAndGetRanking();
        CompareDistanceToNextPoint();

        int[] ranking = Ranking();  // 総合した順位
        int idx = 0;
        foreach (TextMeshProUGUI rankText in rankTextList)
        {
            rankText.text = RankingToString(ranking[idx]);
            imageNo[idx].SetNo(ranking[idx]);
            idx++;
        }
    }

    // 順位ごとに文字付与
    private string RankingToString(int ranking)
    {
        string retString;
        switch(ranking)
        {
            case 1:
                retString = /*ranking.ToString() + */"st";
                break;
            case 2:
                retString = /*ranking.ToString() + */"nd";
                break;
            case 3:
                retString = /*ranking.ToString() + */"rd";
                break;
            default:
                retString = /*ranking.ToString() + */"th";
                break;
        }

        return retString;
    }

    // この↓2つの順位付け関数、中身ほぼ同じなのでなんとかまとめたい

    // 周回数の順位付け
    private void CompareLapCountAndGetRanking()
    {
        GameObject[] racingCars = GameObject.FindGameObjectsWithTag("RacingCar");
        int[] lapCounts = new int[racingCars.Length];
        foreach (GameObject player in racingCars)
        {
            int id = FindInfoByScene.Instance.GetPlayerID(player.transform.parent.name);
            lapCounts[id] = player.GetComponentInChildren<LapCount>().GetLapCount();
//            Debug.Log("プレイヤー:" + id + " 周回数:" + lapCounts[id]);
        }

        Ranking(lapCounts, false);
    }

    // チェックポイント通過数の順位付け
    private void CompareCheckPointCountAndGetRanking()
    {
        GameObject[] racingCars = GameObject.FindGameObjectsWithTag("RacingCar");
        int[] checkPointCounts = new int[racingCars.Length];
        foreach (GameObject player in racingCars)
        {
            int id = FindInfoByScene.Instance.GetPlayerID(player.transform.parent.name);
            checkPointCounts[id] = player.GetComponentInChildren<CheckPointCount>().GetNowThroughCheckPointNum();
//            Debug.Log("プレイヤー:" + id + " チェックポイント通過数:" + checkPointCounts[id]);
        }

        Ranking(checkPointCounts, false);
    }

    //次チェックポイントまでの距離で順位付け
    private void CompareDistanceToNextPoint()
    {
        GameObject[] racingCars = GameObject.FindGameObjectsWithTag("RacingCar");
        GameObject[] checkPoints = GameObject.FindGameObjectsWithTag("CheckPoint");

        // 各プレイヤーのチェックポイントまでの距離(int変換後保存用)
        int[] distanceBlock = new int[racingCars.Length];

        foreach (GameObject player in racingCars)
        {
            int playerID = FindInfoByScene.Instance.GetPlayerID(player.transform.parent.name);

            for (int idx = 0; idx < checkPoints.Length; idx++)
            {
                // 通過数に１足した値を次のチェックポイントとする
                if (checkPoints[idx].name == player.GetComponent<CheckPointCount>().GetNextCPName())
                {
                    // 次のチェックポイントまでの距離
                    float distance = Vector3.Distance(checkPoints[idx].transform.position, player.transform.position);
//                    Debug.Log(playerID + "P Next:" + checkPoints[idx].name + " distance:" + distance);
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

//        Debug.Log("Total.1P" + retRanking[0]);
//        Debug.Log("Total.2P" + retRanking[1]);
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