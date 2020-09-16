using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuScene : MonoBehaviour
{
    struct ButtonInfo
    {
        string name;
        string nextScene;
    };
    [SerializeField] EventSystem eventSystem;
    public Button _battle;
    public Button _timeAtt;
    public Button _exit;
    GameObject _selectObj;
    public AudioSource audioSource;
    public AudioClip sound1;
    public AudioClip sound2;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    bool isCalledOnce = false;

    // Update is called once per frame
    void Update()
    {
        if (!isCalledOnce)
        {
            if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Horizontal"))
            {
                audioSource.PlayOneShot(sound1);
            }
            if (Input.GetButtonDown("Decision") || Input.GetButtonDown("Vertical"))
            {
                audioSource.PlayOneShot(sound2);
            }
            ///ここを任意のボタンにしましょう。
            ///タイムアタック
            if (Input.GetButtonDown("Decision"))
            {
                _selectObj = eventSystem.currentSelectedGameObject.gameObject;
                if (_selectObj.name == _timeAtt.name)
                {
                    FadeManager.Instance.LoadScene("TimeAttackCustom", 1.5f);
                    Debug.Log("TimeCarCustomScene");
                    isCalledOnce = true;
                }
                if (_selectObj.name == _battle.name)
                {
                    FadeManager.Instance.LoadScene("BattleCustom", 1.5f);
                    Debug.Log("BattleCarCustomScene");
                    isCalledOnce = true;
                }
                if(_selectObj.name == _exit.name)
                {
                    #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                    #else
                    Application.Quit();
                    #endif
                }
            }
        }
    }
}
