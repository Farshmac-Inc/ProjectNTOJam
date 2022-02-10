using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class AnimationAsset : TextAsset
{
    [SerializeField] protected ExposedReference<TextProvider> text;
    [SerializeField, TextArea(5,100)] private string textValue;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var template = new AnimationBehaviour();
        template.TextComponent = text.Resolve(graph.GetResolver());
        template.TextValue = textValue;
        template.Clip = Clip;
        template.Director = owner.GetComponent<PlayableDirector>();
        return ScriptPlayable<AnimationBehaviour>.Create(graph, template);
    }
}