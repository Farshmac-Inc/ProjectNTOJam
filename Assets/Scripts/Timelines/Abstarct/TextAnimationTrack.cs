using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(1.0f, 0.0f, 0.0f)]
[TrackClipType(typeof(TextAsset))]
public class TextAnimationTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        var clips = GetClips();
        foreach (var clip in clips)
        {
            var playableAsset = clip.asset as TextAsset;
            playableAsset.Clip = clip;
        }

        return ScriptPlayable<TextBehaviour>.Create(graph, inputCount);
    }
}