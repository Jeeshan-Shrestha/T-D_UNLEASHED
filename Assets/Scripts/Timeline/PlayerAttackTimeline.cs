using System;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerAttackTimeline : MonoBehaviour
{
    [SerializeField] PlayableDirector player1AttackTimeline;
    [SerializeField] PlayableDirector player2AttackTimeline;
    [SerializeField] PlayableDirector player1DodgeTimeline;
    [SerializeField] PlayableDirector player2DodgeTimeline;
    [SerializeField] PlayableDirector player1FinisherTimeline;
    [SerializeField] PlayableDirector player2FinisherTimeline;
    public TextFade fade;
    [SerializeField] private Transform player1Transform;
    [SerializeField] private Transform player2Transform;
    [SerializeField] private Transform cameraTransform;
    private Vector3 player1Pos, player2Pos, cameraPos;
    private Quaternion player1Rot, player2Rot, cameraRot;
    public event Action OnFinisherFinished;

    public void PlayPlayer1AttackTimeline(Action onComplete = null) => Play(player1AttackTimeline, onComplete: onComplete);
    public void PlayPlayer2AttackTimeline(Action onComplete = null) => Play(player2AttackTimeline, onComplete: onComplete);
    public void PlayPlayer1DodgeTimeline(Action onComplete = null)  => Play(player1DodgeTimeline, onComplete: onComplete);
    public void PlayPlayer2DodgeTimeline(Action onComplete = null)  => Play(player2DodgeTimeline, onComplete: onComplete);
    public void PlayPlayer1FinisherTimeline() => Play(player1FinisherTimeline, isFinisher: true);
    public void PlayPlayer2FinisherTimeline() => Play(player2FinisherTimeline, isFinisher: true);

    private void Play(PlayableDirector director, bool isFinisher = false, Action onComplete = null)
    {
        SnapshotTransforms();
        director.Play();
        void Handler(PlayableDirector pd)
        {
            director.stopped -= Handler;
            OnTimelineFinished(pd, isFinisher, onComplete);
        }
        director.stopped += Handler;
    }

    private void SnapshotTransforms()
    {
        player1Pos = player1Transform.position;
        player1Rot = player1Transform.rotation;
        player2Pos = player2Transform.position;
        player2Rot = player2Transform.rotation;
        cameraPos = cameraTransform.position;
        cameraRot = cameraTransform.rotation;
    }

    private void OnTimelineFinished(PlayableDirector pd, bool isFinisher, Action onComplete)
    {
        player1Transform.SetPositionAndRotation(player1Pos, player1Rot);
        player2Transform.SetPositionAndRotation(player2Pos, player2Rot);
        cameraTransform.SetPositionAndRotation(cameraPos, cameraRot);
        if (isFinisher)
            OnFinisherFinished?.Invoke();

        onComplete?.Invoke();
    }
}