using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// 周回数の表示を更新する
public class LapTextUpdate : MonoBehaviour
{
    public TextMeshProUGUI lapText;

    public int playerNo;
    private LapCount _lapCount;
    private int _lapMax;

    void Start()
    {
        _lapCount = GameObject.FindGameObjectsWithTag("RacingCar")[playerNo].gameObject.GetComponent<LapCount>();
        _lapMax = FindInfoByScene.Instance.GetLapMax(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        int _lapCnt = _lapCount.GetLapCount();
        if (_lapCnt <= _lapMax)
        {
            lapText.text = _lapCnt + " / " + _lapMax;
        }
    }
}
