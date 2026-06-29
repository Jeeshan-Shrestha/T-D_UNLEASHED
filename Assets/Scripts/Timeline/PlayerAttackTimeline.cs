using UnityEngine;
using UnityEngine.Playables;

public class PlayerAttackTimeline : MonoBehaviour
{
    [SerializeField] PlayableDirector player1AttackTimeline;
    public TextFade fade;


    public void PlayPlayer1Timeline()
    {
        player1AttackTimeline.Play();
        player1AttackTimeline.stopped += OnTimelineFinished;
    }
    void OnTimelineFinished(PlayableDirector pd)
    {
        StartCoroutine(fade.FadeRoutine());
    }

    void OnDestroy()
    {
        player1AttackTimeline.stopped -= OnTimelineFinished;
    }
}