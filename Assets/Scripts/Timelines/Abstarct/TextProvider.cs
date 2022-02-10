using System;
using UnityEngine;
using UnityEngine.Events;

public class TextProvider : MonoBehaviour
{
    [SerializeField] private UnityEventString textChanged;

    public void SetText(string text) => textChanged?.Invoke(text);

    [Serializable]
    private class UnityEventString : UnityEvent<string>
    {
    }
}