using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingCarMultiSelect : MonoBehaviour
{
    int carIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        CarMultiCustom multiCustom = GameObject.Find("CarMultiCustom").GetComponent<CarMultiCustom>();
        Debug.Log("RacingCarSelectStart");
        if(transform.name == "RacingCar1P")
        {
            carIndex = multiCustom.GetCarID_1();

            GameObject raceCar = transform.GetChild(carIndex).gameObject;
            raceCar.SetActive(true);
        }
        else
        {
            if(multiCustom.GetJoyPad() > 1)
            {
                if (transform.name == "RacingCar2P")
                {
                    carIndex = multiCustom.GetCarID_2();

                    GameObject raceCar = transform.GetChild(carIndex).gameObject;
                    raceCar.SetActive(true);
                }
            }
            else
            {
                if (transform.name == "RacingCarCPU")
                {
                    carIndex = multiCustom.GetCarID_2();

                    GameObject raceCar = transform.GetChild(carIndex).gameObject;
                    raceCar.SetActive(true);
                }
            }
        }

    }
}
