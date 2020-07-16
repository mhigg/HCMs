using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewpointChange : MonoBehaviour
{
    [SerializeField] private GameObject _pivotCamera;
    [SerializeField] private GameObject _insideCamera;
    [SerializeField] private GameObject _frontCamera;
    [SerializeField] private GameObject _upsideCamera;
    private int _keyCount;

    // Start is called before the first frame update
    void Start()
    {
        _pivotCamera.SetActive(true);
        _insideCamera.SetActive(false);
        _frontCamera.SetActive(false);
        _upsideCamera.SetActive(false);

        _keyCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ++_keyCount;
        }
        if (_keyCount >= 4) _keyCount = 0;

        switch (_keyCount)
        {
            case 0:
                _pivotCamera.SetActive(true);
                _insideCamera.SetActive(false);
                _frontCamera.SetActive(false);
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
                break;
        }

    }
}
