using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;

public class OutputCarSpeed : MonoBehaviour
{
    //　スタンダードアセットのCarの速度を保持しているスクリプト
    private CarController carController;
    [SerializeField]
    private Image speedImage = null;
    [SerializeField]
    private Text speedText = null;
    [SerializeField]
    private float percentage = 0f;

    // Start is called before the first frame update
    void Start()
    {
        carController = GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        //　Imageの表示率を最大速度に対する現在の速度で計算する
        var ratio = Mathf.InverseLerp(0f, 1f, Mathf.Abs(carController.CurrentSpeed) / 140f);
        //　速度用のImageの最小と最大を補正した値で計算
        speedImage.fillAmount = Mathf.Lerp(percentage / 140f, (140f - percentage) / 140f, ratio);
        //　現在の速度をテキストに表示する
        speedText.text = Mathf.Abs(carController.CurrentSpeed).ToString("0") + "km/h";
    }
}
