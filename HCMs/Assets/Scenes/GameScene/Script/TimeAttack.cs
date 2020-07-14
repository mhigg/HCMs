using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeAttack : MonoBehaviour
{
    //public FadeManager fadeManager = null;
    public TimeCount timeCounter = null;

    public TimeRanking timeRanking = null;
    public GoalFlag goalFlag = null;

    public StartStopController startCounter = null;

    private string _activeStageName;    // 現在のステージ名

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("TimeAttackStart");
        _activeStageName = SceneManager.GetActiveScene().name;

        int rapMax = FindInfoByScene.Instance.GetRapMax(_activeStageName);
        Debug.Log("rapMax:" + rapMax);

        timeRanking = timeRanking.GetComponent<TimeRanking>();
        // 第一引数をコース名にする
        timeRanking.SetUpTimeRanking(_activeStageName, 11, rapMax);

        goalFlag = goalFlag.GetComponent<GoalFlag>();

        timeCounter = timeCounter.GetComponent<TimeCount>();

        startCounter = startCounter.GetComponent<StartStopController>();
    }

    private bool _isCalledOnce = false; // リザルトシーン遷移フラグ
    private bool _startCall = false;    // カウントスタートのフラグ
    private float _afterTime = 0.0f;    // ゴール後タイムカウント

    // Update is called once per frame
    void Update()
    {
        if (goalFlag.CheckFinish())
        {
            Debug.Log("Spaceキーを押してリザルトへ");
            if (!_isCalledOnce)
            {
                ///ここを任意のボタンにしましょう。
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    string stageNo = _activeStageName.Substring(_activeStageName.Length - 2);
                    _isCalledOnce = true;
                    Debug.Log("Result" + stageNo);
                    FadeManager.Instance.LoadScene("TimeAttackResult" + stageNo, 2.0f);
                }

                if (_afterTime > 10.0f)
                {
                    string stageNo = _activeStageName.Substring(_activeStageName.Length - 2);
                    _isCalledOnce = true;
                    Debug.Log("Result" + stageNo);
                    FadeManager.Instance.LoadScene("TimeAttackResult" + stageNo, 2.0f);
                }

                _afterTime += Time.deltaTime;
            }
        }

        // Debug用　横転等で動けなくなったとき用
        if (!_isCalledOnce)
        {
            ///ここを任意のボタンにしましょう。
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _isCalledOnce = true;
                FadeManager.Instance.LoadScene(_activeStageName, 2.0f);
                Debug.Log("再スタート");
            }
        }

        // 特定のタイミングでStartCountを呼ぶ
        if (!_startCall)
        {
            if (!(startCounter.startWait))
            {
                Debug.Log("レーススタート");
                _startCall = true;
                timeCounter.StartCount();
            }
        }
    }
}
