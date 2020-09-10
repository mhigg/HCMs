using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class CarCustom : MonoBehaviour
{
    [SerializeField] private List<GameObject> _carObj;
    private int _carIndex = 0;
    private int _idxMax;
    private Vector3 _roteVec;
    private bool _firstRun;

    private bool _nonUpFlag = false;
    private bool _nonDownFlag = false;

    void Start()
    {
        _idxMax = _carObj.Count;
        _roteVec = new Vector3(0f, 0.5f, 0f);
        _firstRun = true;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "TimeAttackCustom")
        {
            _carObj[_carIndex].gameObject.transform.Rotate(_roteVec);

            CarIndexSelecting();
            ChangeCarModel(_carIndex);
            if (CrossPlatformInputManager.GetButtonDown("Decision"))
            {
                DontDestroyOnLoad(this);
                FadeManager.Instance.LoadScene("TimeAttack02", 2.0f);
            }
        }

        if (SceneManager.GetActiveScene().name == "TimeAttack02")
        {
            if (_firstRun)
            {
                GameObject raceCar = GameObject.Find("RacingCar");
                GameObject raceCarObj = raceCar.transform.Find("RacingCar_" + $"{_carIndex}").gameObject;
                raceCarObj.SetActive(true);

                _firstRun = false;
            }
        }
    }

    private void CarIndexSelecting()
    {
        if (_carIndex < _idxMax - 1)
        {
            if (CrossPlatformInputManager.GetAxisRaw("Horizontal") > 0)
            {
                if (!_nonUpFlag)
                {
                    _carIndex++;
                    _carObj[_carIndex].gameObject.transform.rotation = _carObj[_carIndex - 1].gameObject.transform.rotation;
                    _nonUpFlag = true;
                }
            }
            else
            {
                _nonUpFlag = false;
            }
        }

        if (_carIndex > 0)
        {
            if (CrossPlatformInputManager.GetAxisRaw("Horizontal") < 0)
            {
                if (!(_nonDownFlag))
                {
                    _carIndex--;
                    _carObj[_carIndex].gameObject.transform.rotation = _carObj[_carIndex + 1].gameObject.transform.rotation;
                    _nonDownFlag = true;

                }
            }
            else
            {
                _nonDownFlag = false;
            }
        }
    }

    private void ChangeCarModel(int index)
    {
        for(int i = 0; i < _idxMax; i++)
        {
            _carObj[i].SetActive(false);
        }
        _carObj[index].SetActive(true);
    }
}