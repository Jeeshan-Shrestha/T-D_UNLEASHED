using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] PlayableDirector director;
    [SerializeField]public  GameObject uiPanel;
    [SerializeField] AudioSource bgMusic;

    [SerializeField] Transform cameraPos;

    public TextFade fade;

    Vector3 tempCameraPos;
    Quaternion tempCameraRot;

    void Start()
    {
        uiPanel.SetActive(false);
        bgMusic.Stop();
        tempCameraPos = cameraPos.position;
        tempCameraRot = cameraPos.rotation;
        // director.stopped += OnTimelineFinished;
        OnTimelineFinished(director);
    }

    void OnTimelineFinished(PlayableDirector pd)
    {
        uiPanel.SetActive(true);
        StartCoroutine(fade.FadeRoutine());
        cameraPos.SetPositionAndRotation(tempCameraPos,tempCameraRot);
        bgMusic.Play();
    }

    void OnDestroy()
    {
        director.stopped -= OnTimelineFinished;
    }

}
