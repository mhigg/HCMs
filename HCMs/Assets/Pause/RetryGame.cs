using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class RetryGame : MonoBehaviour
{
    public GameObject player;
    public GameObject OnPanel;
    private bool pauseGame = false;

    void Start()
    {
        OnUnPause();
    }

    public void Update()
    {
        // パッドのボタンでポーズ切り替えする

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame = !pauseGame;

            if (pauseGame == true)
            {
                OnPause();
            }
            else
            {
                OnUnPause();
            }
        }

        if(pauseGame)
        {
            // 左右？でResumeとRetryを切り替え、決定ボタンで決定
        }
        else
        {

        }
    }

    public void OnPause()
    {
        OnPanel.SetActive(true);
        Time.timeScale = 0;
        pauseGame = true;
        //CarControl car = player.GetComponent<CarControl>();
        //car.enabled = false;
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnUnPause()
    {
        OnPanel.SetActive(false);
        Time.timeScale = 1;
        pauseGame = false;
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