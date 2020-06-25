using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollision : MonoBehaviour
{
    GameObject refObj;
    // Start is called before the first frame update
    void Start()
    {
        refObj = GameObject.Find("RacingCar2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter()
    {
        Resporn r1 = refObj.GetComponent<Resporn>();
        r1.SetResBox(this.transform);
        Debug.Log("すり抜けた！");
    }
}
