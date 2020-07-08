using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollision : MonoBehaviour
{
    private int _num = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        var name = other.transform.parent;
        if(name.name == "RacingCar_01")
        {
            _num = 0;
        }
        if (name.name == "RacingCar_02")
        {
            _num = 1;
        }
        GameObject[] refObj = GameObject.FindGameObjectsWithTag("RacingCar");
        foreach (GameObject car in refObj)
        {
            Resporn resporn = car.gameObject.GetComponentInParent<Resporn>();
            resporn.SetResBox(this.transform,_num);
//            Debug.Log("すり抜けた！" + car.name);
        }
    }
}
