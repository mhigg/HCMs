using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
public class CarMultiCustom : MonoBehaviour
{
    [SerializeField] private List<GameObject> _carObj_1P;
    [SerializeField] private List<GameObject> _carObj_2P;
    private int _carIndex1 = 0, _carIndex2 = 0;
    private int _idxMax;
    private Vector3 _roteVec;
    private bool _firstRun;

    private bool _nonUpFlag1 = false, _nonUpFlag2 = false;
    private bool _nonDownFlag1 = false, _nonDownFlag2 = false;

    private string[] _getJoystick;

    void Start()
    {
        _idxMax = _carObj_1P.Count;
        _roteVec = new Vector3(0f, 0.5f, 0f);
        _firstRun = true;
        _getJoystick = Input.GetJoystickNames();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "BattleCustom")
        {
            _carObj_1P[_carIndex1].gameObject.transform.Rotate(_roteVec);
            _carObj_2P[_carIndex2].gameObject.transform.Rotate(-(_roteVec));

            CarIndexSelecting_1P();//1P車体選択

            if(_getJoystick.Length > 1)
            {
                CarIndexSelecting_2P();//2P存在時のみ車体選択
            }

            ChangeCarModel(_carIndex1, _carIndex2);

            if (CrossPlatformInputManager.GetButtonDown("Decision"))
            {
                if (_getJoystick.Length <= 1)
                {
                    _carIndex2 = Random.Range(0, 3);//CPUの車体をランダムで設定
                }

                DontDestroyOnLoad(this);
                FadeManager.Instance.LoadScene("BattleSelectScene", 2.0f);
            }
        }

        for(int i = 0; i < 6; i++)
        {
            if (SceneManager.GetActiveScene().name == "BattleScene_0" + $"{i}")
            {
                if (_firstRun)
                {
                    GameObject raceCar1 = GameObject.Find("RacingCar1P");
                    GameObject raceCarObj1 = raceCar1.transform.Find("RacingCar_" + $"{_carIndex1}").gameObject;
                    raceCarObj1.SetActive(true);

                    if (_getJoystick.Length > 1)
                    {
                        GameObject raceCar2 = GameObject.Find("RacingCar2P");
                        GameObject raceCarObj2 = raceCar1.transform.Find("RacingCar_" + $"{_carIndex2}").gameObject;
                        raceCarObj2.SetActive(true);
                    }
                    else
                    {
                        GameObject raceCarCPU = GameObject.Find("RacingCarCPU");
                        GameObject raceCarObjCPU = raceCar1.transform.Find("RacingCar_" + $"{_carIndex2}").gameObject;
                    }

                    _firstRun = false;
                }
            }
        }
    }

    private void CarIndexSelecting_1P()
    {
        if (_carIndex1 < _idxMax - 1)
        {
            if (CrossPlatformInputManager.GetAxisRaw("Horizontal") > 0)
            {
                if (!_nonUpFlag1)
                {
                    _carIndex1++;
                    _carObj_1P[_carIndex1].gameObject.transform.rotation = _carObj_1P[_carIndex1 - 1].gameObject.transform.rotation;
                    _nonUpFlag1 = true;
                }
            }
            else
            {
                _nonUpFlag1 = false;
            }
        }

        if (_carIndex1 > 0)
        {
            if (CrossPlatformInputManager.GetAxisRaw("Horizontal") < 0)
            {
                if (!(_nonDownFlag1))
                {
                    _carIndex1--;
                    _carObj_1P[_carIndex1].gameObject.transform.rotation = _carObj_1P[_carIndex1 + 1].gameObject.transform.rotation;
                    _nonDownFlag1 = true;

                }
            }
            else
            {
                _nonDownFlag1 = false;
            }
        }
    }

    private void CarIndexSelecting_2P()
    {
        if (_carIndex2 < _idxMax - 1)
        {
            if (CrossPlatformInputManager.GetAxisRaw("Horizontal_2") > 0)
            {
                if (!_nonUpFlag2)
                {
                    _carIndex2++;
                    _carObj_2P[_carIndex2].gameObject.transform.rotation = _carObj_2P[_carIndex2 - 1].gameObject.transform.rotation;
                    _nonUpFlag2 = true;
                }
            }
            else
            {
                _nonUpFlag2 = false;
            }
        }

        if (_carIndex2 > 0)
        {
            if (CrossPlatformInputManager.GetAxisRaw("Horizontal_2") < 0)
            {
                if (!(_nonDownFlag2))
                {
                    _carIndex2--;
                    _carObj_2P[_carIndex2].gameObject.transform.rotation = _carObj_2P[_carIndex2 + 1].gameObject.transform.rotation;
                    _nonDownFlag2 = true;

                }
            }
            else
            {
                _nonDownFlag2 = false;
            }
        }
    }

    private void ChangeCarModel(int index1, int index2)
    {
        for (int i = 0; i < _idxMax; i++)
        {
            _carObj_1P[i].SetActive(false);
            _carObj_2P[i].SetActive(false);
        }
        _carObj_1P[index1].SetActive(true);
        _carObj_2P[index2].SetActive(true);
    }
}
