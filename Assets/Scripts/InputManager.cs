using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public float inputVector { get; private set; }
    private int ButtonID = 0;

    [SerializeField] private float Sensetivity = 10.0f;
    [SerializeField] private UnityEvent<float> SetInputVector;

    private void FixedUpdate()
    {
        switch (ButtonID)
        {
            case -1:
                {
                    inputVector -= Sensetivity;
                    inputVector = Mathf.Clamp(inputVector, -1, 1);
                    break;
                }
            case 1:
                {
                    inputVector += Sensetivity;
                    inputVector = Mathf.Clamp(inputVector, -1, 1);
                    break;
                }
            default:
                {
                    inputVector = Mathf.Lerp(inputVector, 0, 0.5f);
                    break;
                }
        }
        SetInputVector?.Invoke(inputVector);
    }

    public void ButtonClick(int buttonID)
    {
        ButtonID = buttonID;
    }
}
