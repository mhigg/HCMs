using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CustomMovie : MonoBehaviour
{
    [SerializeField] private List<VideoClip> videoClipList;
    [SerializeField] private RenderTexture m_renderTex;
    private VideoPlayer m_videoPlayer;
    private int videoIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(playVideo());
    }

    IEnumerator playVideo(bool firstRun = true)
    {
        int listLen = videoClipList.Count;
        if (videoClipList == null || listLen <= 0)
        {
            Debug.LogError("Assign VideoClips from the Editor");
            yield break;
        }

        if (firstRun)
        {
            GameObject vidHolder = new GameObject("VideoPlayer");
            vidHolder.transform.SetParent(transform);

            m_videoPlayer = vidHolder.AddComponent<VideoPlayer>();

            m_videoPlayer.playOnAwake = true;

            m_videoPlayer.source = VideoSource.VideoClip;

            videoIndex = Random.Range(0, 4);
            m_videoPlayer.clip = videoClipList[videoIndex];

            m_videoPlayer.isLooping = false;

            m_videoPlayer.renderMode = VideoRenderMode.RenderTexture;
            m_videoPlayer.targetTexture = m_renderTex;

            m_videoPlayer.audioOutputMode = VideoAudioOutputMode.Direct;
            
        }

        m_videoPlayer.Prepare();
        while (!m_videoPlayer.isPrepared)
            yield return null;

        m_videoPlayer.Play();//動画を再生

        while (m_videoPlayer.isPlaying)
            yield return null;

        StartCoroutine(playVideo(false));
    }
}
