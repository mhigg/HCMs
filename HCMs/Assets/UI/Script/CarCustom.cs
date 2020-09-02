using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCustom : MonoBehaviour
{
    [SerializeField] private GameObject _carObj;
    Vector3 _roteVec;

    // Start is called before the first frame update
    void Start()
    {
        _roteVec = new Vector3(0f, 1.5f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        _carObj.gameObject.transform.Rotate(_roteVec);
    }
}
