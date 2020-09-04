using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

public class CarCustom : MonoBehaviour
{
    [SerializeField] private List<GameObject> _carObj;
    private int _carIndex = 0;
    private int _idxMax = 2;

    private Vector3 _roteVec;

    void Start()
    {
        _roteVec = new Vector3(0f, 1.5f, 0f);
    }

    void Update()
    {
        _carObj[_carIndex].gameObject.transform.Rotate(_roteVec);

        CarIndexSelecting();
        ChangeCarModel(_carIndex);
        if (CrossPlatformInputManager.GetButtonDown("Decision"))
        {
            DontDestroyOnLoad(_carObj[_carIndex]);
            FadeManager.Instance.LoadScene("TimeAttack01", 2.0f);
        }
    }

    private void CarIndexSelecting()
    {
        if (_carIndex < _idxMax - 1)
        {
            if (CrossPlatformInputManager.GetAxis("Horizontal") > 0)
            {
                _carIndex++;
                _carObj[_carIndex].gameObject.transform.rotation = _carObj[_carIndex - 1].gameObject.transform.rotation;
            }
        }

        if (_carIndex > 0)
        {
            if (CrossPlatformInputManager.GetAxis("Horizontal") < 0)
            {
                _carIndex--;
                _carObj[_carIndex].gameObject.transform.rotation = _carObj[_carIndex + 1].gameObject.transform.rotation;
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