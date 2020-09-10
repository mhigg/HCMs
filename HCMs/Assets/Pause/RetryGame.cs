using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RetryGame : MonoBehaviour
{
    public enum Pause
    {
        Resume,
        Retry
    };
    Pause _pause;

    [SerializeField] private EventSystem _eventSystem;
    private GameObject _selectObj;
    public List<Pause> pauseList;
    private List<string> _buttonNames;

    public GameObject player;
    public GameObject OnPanel;
    private bool _pauseGame = false;


    void Start()
    {
        _buttonNames = new List<string>();
        _buttonNames.Add("ButtonResume");
        _buttonNames.Add("ButtonRetry");

        OnUnPause();
    }

    public void Update()
    {
        // パッドのボタンでポーズ切り替えする

        if (Input.GetButtonDown("Pause"))
        {
            _pauseGame = !_pauseGame;

            if (_pauseGame == true)
            {
                OnPause();
            }
            else
            {
                OnUnPause();
            }
        }

        if (_pauseGame)
        {
            if (_eventSystem.currentSelectedGameObject.gameObject == _selectObj)
            {
                // 選択を移動していない間は何もしない
            }
            else
            {
                // 選択を移動したら現在選んでいるボタンを変更する
                _selectObj = _eventSystem.currentSelectedGameObject.gameObject;
                
                for(int idx = 0; idx < _buttonNames.Count; idx++)
                {
                    if(_selectObj.name == _buttonNames[idx])
                    {
                        _pause = pauseList[idx];
                        break;
                    }
                }
            }

            if(Input.GetButtonDown("Decision"))
            {
                switch(_pause)
                {
                    case Pause.Resume:
                        OnResume();
                        break;
                    case Pause.Retry:
                        OnRetry();
                        break;
                    default:
                        OnResume();
                        break;
                }
            }
        }
        else
        {

        }
    }

    public void OnPause()
    {
        OnPanel.SetActive(true);
        Time.timeScale = 0;
        _pauseGame = true;
        //CarControl car = player.GetComponent<CarControl>();
        //car.enabled = false;
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnUnPause()
    {
        OnPanel.SetActive(false);
        Time.timeScale = 1;
        _pauseGame = false;
        //CarControl car = player.GetComponent<CarControl>();
        //car.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnResume()
    {
        OnUnPause();
    }
}