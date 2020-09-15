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

    private bool _nonUpFlag1 = false, _nonUpFlag2 = false;
    private bool _nonDownFlag1 = false, _nonDownFlag2 = false;
    private bool _idxDecided1 = false, _idxDecided2 = false;

    private string[] _getJoystick;
    int _joystickQuantity = 0;

    public int GetCarID_1 { get { return _carIndex1; } }
    public int GetCarID_2 { get { return _carIndex2; } }
    public int GetJoyPad { get { return _joystickQuantity; } }
    public bool GetDecidedFlag_1 { get { return _idxDecided1; } }
    public bool GetDecidedFlag_2 { get { return _idxDecided2; } }


    void Start()
    {
        _idxMax = _carObj_1P.Count;
        _roteVec = new Vector3(0f, 0.5f, 0f);

        _getJoystick = Input.GetJoystickNames();
        for (int i = 0; i < _getJoystick.Length; i++)
        {
            if (_getJoystick[i] != "") _joystickQuantity++;  //パッドの接続数取得
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "BattleCustom")
        {
            _carObj_1P[_carIndex1].gameObject.transform.Rotate(_roteVec);
            _carObj_2P[_carIndex2].gameObject.transform.Rotate(-(_roteVec));

            if (CrossPlatformInputManager.GetButtonDown("Decision"))
            {
                _idxDecided1 = true;
                if (_joystickQuantity <= 1)
                {
                    _carIndex2 = Random.Range(0, 3);//CPUの車体をランダムで設定
                    _idxDecided2 = true;
                }
            }

            if (CrossPlatformInputManager.GetButtonDown("Cancel")) _idxDecided1 = false;

            if (CrossPlatformInputManager.GetButtonDown("Decision_2")) _idxDecided2 = true;

            if (CrossPlatformInputManager.GetButtonDown("Cancel_2"))
            {
                _idxDecided2 = false;
            }

            if (_idxDecided1 && _idxDecided2)
            {
                if (CrossPlatformInputManager.GetButtonDown("Start"))
                {
                    DontDestroyOnLoad(this);
                    FadeManager.Instance.LoadScene("BattleSelectScene", 2.0f);
                }
            }

            if (!_idxDecided1) CarIndexSelecting_1P();//1P車体選択

            if (!_idxDecided2)
            {
                if (_joystickQuantity > 1) CarIndexSelecting_2P();//2P存在時のみ車体選択
            }

            ChangeCarModel(_carIndex1, _carIndex2);
        }
    }


    private void CarIndexSelecting_1P()
    {
        if (CrossPlatformInputManager.GetAxisRaw("Horizontal") > 0)
        {
            if (!_nonUpFlag1)
            {
                if (_carIndex1 < _idxMax - 1)
                {
                    _carIndex1++;
                    _carObj_1P[_carIndex1].gameObject.transform.rotation = _carObj_1P[_carIndex1 - 1].gameObject.transform.rotation;
                    _nonUpFlag1 = true;
                }
                else
                {
                    _carIndex1 = 0;
                    _carObj_1P[_carIndex1].gameObject.transform.rotation = _carObj_1P[_idxMax - 1].gameObject.transform.rotation;
                    _nonUpFlag1 = true;
                }
            }
        }
        else
        {
            _nonUpFlag1 = false;
        }


        if (CrossPlatformInputManager.GetAxisRaw("Horizontal") < 0)
        {
            if (!(_nonDownFlag1))
            {
                if (_carIndex1 > 0)
                {
                    _carIndex1--;
                    _carObj_1P[_carIndex1].gameObject.transform.rotation = _carObj_1P[_carIndex1 + 1].gameObject.transform.rotation;
                    _nonDownFlag1 = true;
                }
                else
                {
                    _carIndex1 = _idxMax - 1;
                    _carObj_1P[_carIndex1].gameObject.transform.rotation = _carObj_1P[0].gameObject.transform.rotation;
                    _nonDownFlag1 = true;
                }

            }
        }
        else
        {
            _nonDownFlag1 = false;
        }

    }

    private void CarIndexSelecting_2P()
    {
        if (CrossPlatformInputManager.GetAxisRaw("Horizontal_2") > 0)
        {
            if (!_nonUpFlag2)
            {
                if (_carIndex2 < _idxMax - 1)
                {
                    _carIndex2++;
                    _carObj_2P[_carIndex2].gameObject.transform.rotation = _carObj_2P[_carIndex2 - 1].gameObject.transform.rotation;
                    _nonUpFlag2 = true;
                }
                else
                {
                    _carIndex2 = 0;
                    _carObj_2P[_carIndex2].gameObject.transform.rotation = _carObj_2P[_idxMax - 1].gameObject.transform.rotation;
                    _nonUpFlag2 = true;

                }
            }
        }
        else
        {
            _nonUpFlag2 = false;
        }

        if (CrossPlatformInputManager.GetAxisRaw("Horizontal_2") < 0)
        {
            if (!(_nonDownFlag2))
            {
                if (_carIndex2 > 0)
                {
                    _carIndex2--;
                    _carObj_2P[_carIndex2].gameObject.transform.rotation = _carObj_2P[_carIndex2 + 1].gameObject.transform.rotation;
                    _nonDownFlag2 = true;
                }
                else
                {
                    _carIndex2 = _idxMax - 1;
                    _carObj_2P[_carIndex2].gameObject.transform.rotation = _carObj_2P[0].gameObject.transform.rotation;
                    _nonDownFlag2 = true;

                }
            }
        }
        else
        {
            _nonDownFlag2 = false;
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
