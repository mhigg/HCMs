using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Score _score;

    void Start()
    {
        //あとで
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string json = JsonUtility.ToJson(_score);
            print(json);
        }
    }
}