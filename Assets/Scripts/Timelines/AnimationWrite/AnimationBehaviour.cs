using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class AnimationBehaviour : TextBehaviour
{
    public string TextValue;

    public override void OnPlayableCreate(Playable playable)
    {
        if (ComponentNotConfigured())
        {
            return;
        }

        TextComponent.SetText(string.Empty);
    }

    public override void OnBehaviourPause(Playable playable,
        FrameData info)
    {
        if (ComponentNotConfigured())
        {
            return;
        }

        TextComponent.SetText(Director.time > Clip.start
            ? TextValue
            : string.Empty);
    }

    public override void ProcessFrame(Playable playable,
        FrameData info,
        object playerData)
    {
        if (ComponentNotConfigured())
        {
            return;
        }

        TextComponent.SetText(TextValue
        .Substring(0, (int) (TextValue.Length * (playable.GetPreviousTime() / playable.GetDuration()))));
    }

    protected bool ComponentNotConfigured()
    {
        return Clip == null || TextValue == null || TextComponent == null;
    }
}