using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MultiMovie : MonoBehaviour
{
	public List<VideoClip> videoClipList;
	public bool loop;
	[HideInInspector] private List<VideoPlayer> videoPlayerList;
	[HideInInspector] private int videoIndex = 0;

	void Start()
	{
		StartCoroutine(playVideo());    // コルーチンの呼び出し
	}
	
	IEnumerator playVideo(bool firstRun = true)
	{   // コルーチンの宣言
		int listLen = videoClipList.Count;
		if (videoClipList == null || listLen <= 0)
		{
			Debug.LogError("Assign VideoClips from the Editor");
			yield break;
		}

		if (loop)   // ループの処理
			videoIndex %= listLen;  // インデックスを戻す。
		else
		{
			if (videoIndex >= listLen)
				yield break;    // 全再生が終了したら、ここでコルーチンを途中終了する。
		}

		if (firstRun)
		{   // 最初に呼び出された時にのみ実行する処理
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

				videoPlayer.renderMode = VideoRenderMode.MaterialOverride;  // レンダリング方法の設定
				videoPlayer.targetMaterialRenderer = GetComponent<Renderer>();  // レンダリング先の設定

				videoPlayer.audioOutputMode = VideoAudioOutputMode.Direct;  // 音声の出力方法の設定
			}
		}

		videoPlayerList[videoIndex].Prepare();  // 再生に際した準備をする。
		while (!videoPlayerList[videoIndex].isPrepared)
			yield return null;  // 準備中は、ここでコルーチンを中断する。

		videoPlayerList[videoIndex].Play(); // 動画を再生する。

		while (videoPlayerList[videoIndex].isPlaying)
			yield return null;  // 再生中は、ここでコルーチンを中断する。

		videoPlayerList[videoIndex].Stop(); // 動画の時刻を0に戻す。
		videoIndex++;   // インデックスを進める。

		StartCoroutine(playVideo(false));   // 反復処理中に呼び出された時は、中盤の処理を省く。
	}
}