using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter()
    {
        GameObject[] refObj = GameObject.FindGameObjectsWithTag("RacingCar");
        foreach (GameObject car in refObj)
        {
            Resporn resporn = car.gameObject.GetComponentInParent<Resporn>();
            resporn.SetResBox(this.transform);
//            Debug.Log("すり抜けた！" + car.name);
        }
    }
}
