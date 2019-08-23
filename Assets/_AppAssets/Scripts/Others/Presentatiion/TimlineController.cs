using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimlineController : MonoBehaviour
{

    public List<PlayableDirector> playableDirectors;
    public List<TimelineAsset> timelines;
    public int currentTimeline;
    private void Start()
    {
        PlayFromTimelines();
    }
    public void Play()
    {
        foreach (PlayableDirector playableDirector in playableDirectors)
        {
            playableDirector.Play();
        }
    }

    public void PlayFromTimelines()
    {

        TimelineAsset selectedAsset;
        currentTimeline =  Mathf.Clamp(currentTimeline,0, timelines.Count - 1);
        selectedAsset = timelines[currentTimeline];
        playableDirectors[0].Play(selectedAsset);

    }
    public void playSlide(int slideNumber) {
        this.currentTimeline = slideNumber - 1;
        PlayFromTimelines();
    }
}