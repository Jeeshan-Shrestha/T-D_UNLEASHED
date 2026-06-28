using System;
using UnityEngine;
using UnityEngine.Playables;

public class UIManager : MonoBehaviour
{
    [SerializeField] PlayableDirector director;
    [SerializeField] GameObject uiPanel;
    [SerializeField] AudioSource bgMusic;

    public TextFade fade;

    void Start()
    {
        uiPanel.SetActive(false);
        bgMusic.Stop();
        director.stopped += OnTimelineFinished;
    }

    void OnTimelineFinished(PlayableDirector pd)
    {
        uiPanel.SetActive(true);
        StartCoroutine(fade.FadeRoutine());
        bgMusic.Play();
    }

    void OnDestroy()
    {
        director.stopped -= OnTimelineFinished;
    }

}
