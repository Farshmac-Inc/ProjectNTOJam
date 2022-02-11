using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Country : NeedStopObject
{
    public UnityEvent<float, Sprite> UnloadingFinish;    
    [SerializeField] private Sprite carModel;
    [SerializeField] private float NeededCargoMass = 2.0f;

    public override void Result()
    {
        UnloadingFinish?.Invoke(NeededCargoMass, carModel);
        UnloadingFinish.RemoveAllListeners();
    }
}
