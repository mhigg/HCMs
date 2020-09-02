using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCustom : MonoBehaviour
{
    [SerializeField] private GameObject _carObj;
    Vector3 _roteVec;

    void Start()
    {
        _roteVec = new Vector3(0f, 1.5f, 0f);
    }

    void Update()
    {
        _carObj.gameObject.transform.Rotate(_roteVec);
        ModelSelecting();
    }

    void ModelSelecting()
    {
        DontDestroyOnLoad(_carObj);
    }
}
