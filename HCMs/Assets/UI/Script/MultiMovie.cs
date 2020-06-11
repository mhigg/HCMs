using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MultiMovie : MonoBehaviour
{
	public List<VideoClip> videoClipList;
	public bool loop;
	[HideInInspector] private List<VideoPlayer> videoPlayerList;
	[HideInInspector] private int videoIndex = 0;
	[SerializeField] EventSystem eventSystem;
	[SerializeField] RenderTexture _texture;
	Button _timeAtt;
	Button _battle;
	Button _obst;
    Button _operate;
    GameObject _operateTex;
	GameObject _selectObj;
	bool firstRun = true;

	void Start()
	{
		_timeAtt = GameObject.Find("/Canvas/TimeAttack").GetComponent<Button>();
		_battle = GameObject.Find("/Canvas/Battle").GetComponent<Button>();
		_obst = GameObject.Find("/Canvas/Obs").GetComponent<Button>();
        _operate = GameObject.Find("/Canvas/Operation").GetComponent<Button>();

        _operateTex = GameObject.Find("OperationImage");
        _operateTex.SetActive(false);

        _timeAtt.Select();
	}

	private void Update()
	{
		int listLen = videoClipList.Count;
		// 動画が一つもなければエラー表示
		if (videoClipList == null || listLen <= 0)
		{
			Debug.LogError("Assign VideoClips from the Editor");
		}
		//動画の設定（初回のみ）
		if (firstRun)
		{   // ビデオの設定
			videoPlayerList = new List<VideoPlayer>();
			for (int i = 0; i < listLen; i++)
			{
				GameObject vidHolder = new GameObject("VP" + i);
				vidHolder.transform.SetParent(transform);

				VideoPlayer videoPlayer = vidHolder.AddComponent<VideoPlayer>();    // videoPlayeコンポーネントの追加
				videoPlayerList.Add(videoPlayer);

				videoPlayer.playOnAwake = false;    // trueとすると、音声と動画がずれてしまう。

				videoPlayer.source = VideoSource.VideoClip; // 動画ソースの設定
				videoPlayer.clip = videoClipList[i];

				videoPlayer.isLooping = false;  // trueとすると、次のVideoClipが再生されない。

				videoPlayer.renderMode = VideoRenderMode.RenderTexture;  // レンダリング方法の設定
				videoPlayer.targetTexture = _texture;  // レンダリング先の設定

				videoPlayer.audioOutputMode = VideoAudioOutputMode.Direct;  // 音声の出力方法の設定

            }
			firstRun = false;
		}
		if (eventSystem.currentSelectedGameObject.gameObject == _selectObj)
		{
		}
		else
		{
			videoPlayerList[videoIndex].Stop();
			_selectObj = eventSystem.currentSelectedGameObject.gameObject;
			if (_selectObj.name == _timeAtt.name)
			{
				videoIndex = 0;
			}
			if (_selectObj.name == _battle.name)
			{
				videoIndex = 1;
			}
			if (_selectObj.name == _obst.name)
			{
				videoIndex = 2;
			}
            if(_selectObj.name == _operate.name)
            {
                videoPlayerList[videoIndex].Pause();
                _operateTex.SetActive(true);//操作説明画像表示
            }
            else
            {
                _operateTex.SetActive(false);//操作説明非画像表示
            }
			videoPlayerList[videoIndex].Prepare();
			videoPlayerList[videoIndex].Play();
		}
	}
}