using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class InputArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] InputManager inputManager;
    [SerializeField] private bool IsRightArea;
    public void OnPointerDown(PointerEventData eventData)
    {
        inputManager.ButtonClick(IsRightArea ? 1 : -1);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputManager.ButtonClick(0);
    }
}
