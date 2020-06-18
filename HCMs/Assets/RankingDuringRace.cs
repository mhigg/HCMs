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

    // Start is called before the first frame update
    void Start()
    {
        checkPointCount = checkPointCount.GetComponent<CheckPointCount>();
        rankText01 = rankText01.GetComponent<TextMeshProUGUI>();
        rankText02 = rankText02.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        CompareRapCount();

        int[] cpRanking = checkPointCount.CompareCountAndGetRankOfCheckPointCount();
        rankText01.text = cpRanking[0].ToString();
        rankText02.text = cpRanking[1].ToString();
    }

    private void CompareRapCount()
    {
        GameObject[] racingCars = GameObject.FindGameObjectsWithTag("RacingCar");
        int[] rapCounts = new int[racingCars.Length];
        foreach (GameObject player in racingCars)
        {
            int id = int.Parse(player.name);
            rapCounts[id] = player.GetComponentInChildren<RapCount>().GetRapCount();
            Debug.Log("プレイヤー:" + id + " 周回数:" + rapCounts[id]);
        }
    }
}
