using UnityEngine.SceneManagement;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ViewpointChange2 : MonoBehaviour
{
    [SerializeField] private GameObject _pivotCamera;
    [SerializeField] private GameObject _frontCamera;
    [SerializeField] private GameObject _upsideCamera;
    private int _keyCount;  //切り替えキーを押した回数

    void Start()
    {
        _pivotCamera.SetActive(true);
        _frontCamera.SetActive(false);
        _upsideCamera.SetActive(false);

        _keyCount = 0;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "BattleCustom"
            && SceneManager.GetActiveScene().name != "TimeAttackCustom")
        {
            ViewChanging();
        }
    }

    private void ViewChanging()
    {
        if (CrossPlatformInputManager.GetButtonDown("Camera"))
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
                _frontCamera.SetActive(true);
                break;
            case 2:
                _frontCamera.SetActive(false);
                _upsideCamera.SetActive(true);
                break;
            default:
                _keyCount = 0;//0～2以外は0に戻す
                break;
        }
    }
}
