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
	GameObject _selectObj;
	bool firstRun = true;

	void Start()
	{
		_timeAtt = GameObject.Find("/Canvas/TimeAttack").GetComponent<Button>();
		_battle = GameObject.Find("/Canvas/Battle").GetComponent<Button>();
		_obst = GameObject.Find("/Canvas/Obs").GetComponent<Button>();

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
			videoPlayerList[videoIndex].Prepare();
			videoPlayerList[videoIndex].Play();
		}
	}

	//IEnumerator playVideo(bool firstRun = true)
	//{
	//	////あってもなくてもかわらん
	//	//if (Input.GetMouseButton(0))
	//	//{
	//	//	try
	//	//	{
	//	//		_selectObj = eventSystem.currentSelectedGameObject.gameObject;
	//	//	}
	//	//	// 例外処理的なやつ
	//	//	catch (NullReferenceException ex)
	//	//	{
	//	//	}

	//	//	if (_selectObj == _timeAtt)
	//	//	{
	//	//		videoIndex = 0;
	//	//	}
	//	//	if (_selectObj == _battle)
	//	//	{
	//	//		videoIndex = 1;
	//	//	}
	//	//	if (_selectObj == _obst)
	//	//	{
	//	//		videoIndex = 2;
	//	//	}
	//	//}
	//	// コルーチンの宣言
	//	int listLen = videoClipList.Count;
	//	if (videoClipList == null || listLen <= 0)
	//	{
	//		Debug.LogError("Assign VideoClips from the Editor");
	//		yield break;
	//	}

	//	if (loop)   // ループの処理
	//		videoIndex %= listLen;  // インデックスを戻す。
	//	else
	//	{
	//		if (videoIndex >= listLen)
	//			yield break;    // 全再生が終了したら、ここでコルーチンを途中終了する。
	//	}

	//	if (firstRun)
	//	{   // ビデオの設定
	//		videoPlayerList = new List<VideoPlayer>();
	//		for (int i = 0; i < listLen; i++)
	//		{
	//			GameObject vidHolder = new GameObject("VP" + i);
	//			vidHolder.transform.SetParent(transform);

	//			VideoPlayer videoPlayer = vidHolder.AddComponent<VideoPlayer>();    // videoPlayeコンポーネントの追加
	//			videoPlayerList.Add(videoPlayer);

	//			videoPlayer.playOnAwake = false;    // trueとすると、音声と動画がずれてしまう。

	//			videoPlayer.source = VideoSource.VideoClip; // 動画ソースの設定
	//			videoPlayer.clip = videoClipList[i];

	//			videoPlayer.isLooping = false;  // trueとすると、次のVideoClipが再生されない。

	//			videoPlayer.renderMode = VideoRenderMode.RenderTexture;  // レンダリング方法の設定
	//			videoPlayer.targetTexture = _texture;  // レンダリング先の設定
				
	//			videoPlayer.audioOutputMode = VideoAudioOutputMode.Direct;  // 音声の出力方法の設定
	//		}
	//	}

	//	videoPlayerList[videoIndex].Prepare();  // 再生に際した準備をする。
	//	while (!videoPlayerList[videoIndex].isPrepared)
	//		yield return null;  // 準備中は、ここでコルーチンを中断する。

	//	videoPlayerList[videoIndex].Play(); // 動画を再生する。

	//	while (videoPlayerList[videoIndex].isPlaying)
	//		yield return null;  // 再生中は、ここでコルーチンを中断する。

	//	videoPlayerList[videoIndex].Stop(); // 動画の時刻を0に戻す。

	//	StartCoroutine(playVideo(false));   // 反復処理中に呼び出された時は、中盤の処理を省く。
	//}
}