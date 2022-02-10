using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public abstract class TextAsset : PlayableAsset
{
    [HideInInspector] public TimelineClip Clip;
}
