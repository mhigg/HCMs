using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingCarSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("RacingCarSelectStart");
        int carIndex = GameObject.Find("CarCustom").GetComponent<CarCustom>().GetCarID;

        GameObject raceCar = transform.GetChild(carIndex).gameObject;
        raceCar.SetActive(true);
    }
}
