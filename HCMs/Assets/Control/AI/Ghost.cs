using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    List<float> _tmpH = new List<float>();
    List<float> _tmpV = new List<float>();
    List<List<float>> _key;
    int _nowFrame = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int a = 0;a < 9000;a++)
        {
            _tmpH.Add(0);
            _tmpV.Add(0);
        }
        _key = new List<List<float>>() {
        _tmpH,    // Horizontal
        _tmpV     // Vertical
        };
        _nowFrame = 0;
    }

    // Update is called once per frame
    void Update()
    {        
        _tmpH[_nowFrame] = Input.GetAxis("Horizontal");
        _tmpV[_nowFrame] = Input.GetAxis("Vertical");
        Debug.Log(_tmpH[_nowFrame]);
        _nowFrame++;        
    }
}
