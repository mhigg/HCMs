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

    private FindInfoByScene findInfoByScene = FindInfoByScene.Instance;

    // Start is called before the first frame update
    void Start()
    {
        _playerNum = findInfoByScene.GetPlayerNum;
        _rankingList = new List<int[]>(_playerNum);

        for (int playerID = 0; playerID < _playerNum && playerID < rankTextList.Count; playerID++)
        {
            // 順位表示の初期化
            rankTextList[playerID].text = RankingToString(playerID + 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 周回数→チェックポイント通過数→次チェックポイントまでの距離の順にランク付けする
        CompareCountsAndGetRanking(1, false);
        CompareCountsAndGetRanking(2, false);
        CompareDistanceToNextPoint();

        int[] ranking = Ranking();  // 総合した順位

        int canvasNo;
        // 順位表示処理
        for (int idx = 0; idx < findInfoByScene.GetPlayerNum; idx++)
        {
            GameObject racingCar = GameObject.Find(findInfoByScene.GetPlayerName(idx));
            string playerName = racingCar.transform.parent.name;
            if(playerName != "RacingCarCPU")
            {
                if (playerName == "RacingCar" || playerName == "RacingCar1P")
                {
                    canvasNo = 0;
                }
                else
                {
                    canvasNo = 1;
                }

                rankTextList[canvasNo].text = RankingToString(ranking[idx]);
                imageNo[canvasNo].SetNo(ranking[idx]);
            }
        }
    }

    // 順位ごとに文字付与
    private string RankingToString(int ranking)
    {
        string retString;
        switch(ranking)
        {
            case 1:
                retString = "st";
                break;
            case 2:
                retString = "nd";
                break;
            case 3:
                retString = "rd";
                break;
            default:
                retString = "th";
                break;
        }

        return retString;
    }

    // 周回数とチェックポイント通過数の順位付け
    private void CompareCountsAndGetRanking(int countIdx, bool isAsc)
    {
        GameObject[] racingCars = GameObject.FindGameObjectsWithTag("RacingCar");
        int[] counts = new int[racingCars.Length];

        foreach (GameObject player in racingCars)
        {
            // プレイヤー名からプレイヤーIDを取得
            int id = findInfoByScene.GetPlayerID(player.transform.parent.name);
            switch(countIdx)
            {
                case 1:
                    // 周回数の順位付け
                    counts[id] = player.GetComponentInChildren<LapCount>().GetLapCount();
                    break;
                case 2:
                    // チェックポイント通過数の順位付け
                    counts[id] = player.GetComponentInChildren<CheckPointCount>().GetNowThroughCheckPointNum();
                    break;
                default:
                    break;
            }
        }

        Ranking(counts, isAsc);
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
            int playerID = findInfoByScene.GetPlayerID(player.transform.parent.name);

            for (int idx = 0; idx < checkPoints.Length; idx++)
            {
                // 通過数に１足した値を次のチェックポイントとする
                if (checkPoints[idx].name == player.GetComponent<CheckPointCount>().GetNextCPName())
                {
                    // 次のチェックポイントまでの距離
                    float distance = Vector3.Distance(checkPoints[idx].transform.position, player.transform.position);
                    // Debug.Log(playerID + "P Next:" + checkPoints[idx].name + " distance:" + distance);
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
            // 順位の初期化
            retRanking[rank] = 1;
        }

        // あらかじめ集計した順位情報をもとに総合的な順位付けを行う
        for (int playerID = 0; playerID < _playerNum; playerID++)
        {
            for (int comparison = 0; comparison < _playerNum; comparison++)
            {
                if (isAsc)
                {
                    // 昇順
                    if (playerID != comparison && counts[comparison] < counts[playerID])
                    {
                        // プレイヤーが比較相手より順位が高ければ総合順位を下げる
                        retRanking[playerID]++;
                    }
                }
                else
                {
                    // 降順
                    if (playerID != comparison && counts[playerID] < counts[comparison])
                    {
                        // プレイヤーが比較相手より順位が低ければ総合順位を下げる
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