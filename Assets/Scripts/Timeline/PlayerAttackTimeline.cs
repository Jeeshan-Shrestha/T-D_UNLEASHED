using UnityEngine;
using UnityEngine.Playables;

public class PlayerAttackTimeline : MonoBehaviour
{
    [SerializeField] PlayableDirector player1AttackTimeline;
    [SerializeField] PlayableDirector player2AttackTimeline;
    [SerializeField] PlayableDirector player1DodgeTimeline;
    [SerializeField] PlayableDirector player2DodgeTimeline;
    [SerializeField] PlayableDirector player1FinisherTimeline;
    public TextFade fade;

    [SerializeField] private Transform player1Transform;
    [SerializeField] private Transform player2Transform;
    [SerializeField] private Transform cameraTransform;

    private Vector3 player1Pos;
    private Vector3 player2Pos;
    private Quaternion player1Rot;
    private Quaternion player2Rot;

    private Vector3 cameraPos;
    private Quaternion cameraRot;

    public void PlayPlayer1AttackTimeline()
    {
        player1Pos = player1Transform.position;
        player1Rot = player1Transform.rotation;
        
        player2Pos = player2Transform.position;
        player2Rot = player2Transform.rotation; 

        cameraPos = cameraTransform.position;
        cameraRot = cameraTransform.rotation;

        player1AttackTimeline.Play();
        player1AttackTimeline.stopped += OnPlayer1AttackTimelineFinished;
    }

    public void PlayPlayer2AttackTimeline()
    {
        player1Pos = player1Transform.position;
        player1Rot = player1Transform.rotation;
        
        player2Pos = player2Transform.position;
        player2Rot = player2Transform.rotation; 

        cameraPos = cameraTransform.position;
        cameraRot = cameraTransform.rotation;  
        player2AttackTimeline.Play();
        player2AttackTimeline.stopped += OnPlayer2AttackTimelineFinished;
    }
    public void PlayPlayer1DodgeTimeline()
    {
        player1Pos = player1Transform.position;
        player1Rot = player1Transform.rotation;
        
        player2Pos = player2Transform.position;
        player2Rot = player2Transform.rotation; 

        cameraPos = cameraTransform.position;
        cameraRot = cameraTransform.rotation;  
        player1DodgeTimeline.Play();
        player1DodgeTimeline.stopped += OnPlayer1AttackTimelineFinished;
    }

    public void PlayPlayer2DodgeTimeline()
    {
        player1Pos = player1Transform.position;
        player1Rot = player1Transform.rotation;
        
        player2Pos = player2Transform.position;
        player2Rot = player2Transform.rotation; 

        cameraPos = cameraTransform.position;
        cameraRot = cameraTransform.rotation;  
        player2DodgeTimeline.Play();
        player2DodgeTimeline.stopped += OnPlayer2AttackTimelineFinished;
    }

    public void PlayPlayer1FinisherTimeline()
    {
        player1Pos = player1Transform.position;
        player1Rot = player1Transform.rotation;
        
        player2Pos = player2Transform.position;
        player2Rot = player2Transform.rotation; 

        cameraPos = cameraTransform.position;
        cameraRot = cameraTransform.rotation;

        player1FinisherTimeline.Play();
        player1FinisherTimeline.stopped += OnPlayer1AttackTimelineFinished;
        
    }
 
    void OnPlayer2AttackTimelineFinished(PlayableDirector pd)
    {
        player1Transform.SetPositionAndRotation(player1Pos, player1Rot);
        player2Transform.SetPositionAndRotation(player2Pos, player2Rot);
        cameraTransform.SetPositionAndRotation(cameraPos, cameraRot);
        StartCoroutine(fade.FadeRoutine());   
    }
    void OnPlayer1AttackTimelineFinished(PlayableDirector pd)
    {
        player1Transform.SetPositionAndRotation(player1Pos, player1Rot);
        player2Transform.SetPositionAndRotation(player2Pos, player2Rot);
        cameraTransform.SetPositionAndRotation(cameraPos, cameraRot);
        StartCoroutine(fade.FadeRoutine());
    }

    void OnDestroy()
    {
        player1AttackTimeline.stopped -= OnPlayer1AttackTimelineFinished;
        player2AttackTimeline.stopped -= OnPlayer2AttackTimelineFinished;
    }
}