using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Vehicles.Car;

public class OutputCarSpeed : MonoBehaviour
{
    public ImageNo imageNo = null;

    //　スタンダードアセットのCarの速度を保持しているスクリプト
    private CarController carController;
    [SerializeField]
    private Image speedImage = null;
    [SerializeField]
    private float percentage = 0f;
    private bool firstRun;

    // Start is called before the first frame update
    void Start()
    {
        carController = GetComponent<CarController>();

        firstRun = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "BattleCustom"
        && SceneManager.GetActiveScene().name != "TimeAttackCustom")
        {
            if (firstRun)
            {
                GameObject speedImgNo = GameObject.Find("SpeedImageNo");
                imageNo = speedImgNo.GetComponent<ImageNo>();

                GameObject speedImg = GameObject.Find("SpeedMeterImage");
                speedImage = speedImg.GetComponent<Image>();
            }
        }

        //　Imageの表示率を最大速度に対する現在の速度で計算する
        var ratio = Mathf.InverseLerp(0f, 1f, Mathf.Abs(carController.CurrentSpeed) / 140f);
        //　速度用のImageの最小と最大を補正した値で計算
        speedImage.fillAmount = Mathf.Lerp(percentage / 140f, (140f - percentage) / 140f, ratio);
        //　現在の速度をテキストに表示する
        imageNo.SetNo(Mathf.FloorToInt(carController.CurrentSpeed), "0");
    }
}
