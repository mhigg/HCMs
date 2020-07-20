using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ViewpointChange : MonoBehaviour
{
    [SerializeField] private GameObject _pivotCamera;
    [SerializeField] private GameObject _insideCamera;
    [SerializeField] private GameObject _frontCamera;
    [SerializeField] private GameObject _upsideCamera;
    private int _keyCount;  //切り替えキーを押した回数

    void Start()
    {
        _pivotCamera.SetActive(true);
        _insideCamera.SetActive(false);
        _frontCamera.SetActive(false);
        _upsideCamera.SetActive(false);

        _keyCount = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown("joystick button 1"))
        {
            ++_keyCount;
        }

        switch (_keyCount)
        {
            case 0:
                _pivotCamera.SetActive(true);
                _upsideCamera.SetActive(false);
                break;
            case 1:
                _pivotCamera.SetActive(false);
                _insideCamera.SetActive(true);
                break;
            case 2:
                _insideCamera.SetActive(false);
                _frontCamera.SetActive(true);
                break;
            case 3:
                _frontCamera.SetActive(false);
                _upsideCamera.SetActive(true);
                break;
            default:
                _keyCount = 0;//0～3以外は0に戻す
                break;
        }

    }
}
