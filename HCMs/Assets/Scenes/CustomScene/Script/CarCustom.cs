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

    public int GetCarID { get { return _carIndex; } }

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
                FadeManager.Instance.LoadScene("TimeSelectScene", 1.5f);
            }
        }
    }

    private void CarIndexSelecting()
    {
        if (CrossPlatformInputManager.GetAxisRaw("Horizontal") > 0)
        {
            if (!_nonUpFlag)
            {
                if (_carIndex < _idxMax - 1)
                {
                    _carIndex++;
                    _carObj[_carIndex].gameObject.transform.rotation = _carObj[_carIndex - 1].gameObject.transform.rotation;
                    _nonUpFlag = true;
                }
                else
                {
                    _carIndex = 0;
                    _carObj[_carIndex].gameObject.transform.rotation = _carObj[_idxMax - 1].gameObject.transform.rotation;
                    _nonUpFlag = true;
                }

            }
        }
        else
        {
            _nonUpFlag = false;
        }

        if (CrossPlatformInputManager.GetAxisRaw("Horizontal") < 0)
        {
            if (!(_nonDownFlag))
            {
                if (_carIndex > 0)
                {
                    _carIndex--;
                    _carObj[_carIndex].gameObject.transform.rotation = _carObj[_carIndex + 1].gameObject.transform.rotation;
                    _nonDownFlag = true;
                }
                else
                {
                    _carIndex = _idxMax - 1;
                    _carObj[_carIndex].gameObject.transform.rotation = _carObj[0].gameObject.transform.rotation;
                    _nonDownFlag = true;
                }
            }
        }
        else
        {
            _nonDownFlag = false;
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