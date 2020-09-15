using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Vehicles.Car;

public class OutputCarSpeed : MonoBehaviour
{
    public ImageNo imageNo = null;
    public int playerNo;

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
//        carController = this.GetComponent<CarController>();
        // playerNo = 1ならRacingCar1P、playerNo = 2ならRacingCar2PかCPUの子から
        // アクティブなRacingCarをFindして名前でPlayerID検索する
        carController = GameObject.FindGameObjectsWithTag("RacingCar")[playerNo].transform.parent.gameObject.GetComponent<CarController>();

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
                imageNo = imageNo.GetComponent<ImageNo>();

                speedImage = speedImage.GetComponent<Image>();
            }
        }

        //  各ギアでのメーター最小値の割り当て
        float minSpeed = 0f;
        if (carController.CurrentGearNum > 0)
        {
            minSpeed = carController.MaxSpeed[carController.CurrentGearNum - 1];
        }

        //　Imageの表示率を最大速度に対する現在の速度で計算する
        var ratio = Mathf.InverseLerp(0f, 1f, (Mathf.Abs(carController.CurrentSpeed) - minSpeed) / (carController.MaxSpeed[carController.CurrentGearNum] - minSpeed));

        //　速度用のImageの最小と最大を補正した値で計算
        speedImage.fillAmount
            = Mathf.Lerp(
                percentage / carController.MaxSpeed[carController.CurrentGearNum],
                (carController.MaxSpeed[carController.CurrentGearNum] - percentage) / carController.MaxSpeed[carController.CurrentGearNum],
                ratio);

        //　現在の速度をテキストに表示する
        imageNo.SetNo(Mathf.FloorToInt(carController.CurrentSpeed), "0");

    }
}
