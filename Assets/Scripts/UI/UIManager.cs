using UnityEngine;
using UnityEngine.Playables;

public class UIManager : MonoBehaviour
{
    [SerializeField] PlayableDirector director;
    [SerializeField] public GameObject uiPanel;
    public AudioManager audioManager;
    [SerializeField] Transform cameraPos;
    public GameObject levelSelectButtons; // NEW: reference to level select panel

    Vector3 tempCameraPos;
    Quaternion tempCameraRot;

    void Start()
    {
        uiPanel.SetActive(false);
        tempCameraPos = cameraPos.position;
        tempCameraRot = cameraPos.rotation;

        OnTimelineFinished(director);
    }

    void OnTimelineFinished(PlayableDirector pd)
    {
        uiPanel.SetActive(true);
        cameraPos.SetPositionAndRotation(tempCameraPos, tempCameraRot);
        audioManager.PlayBgMusic();

        levelSelectButtons.SetActive(true); 
    }

    void OnDestroy()
    {
        director.stopped -= OnTimelineFinished;
    }
}