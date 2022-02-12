using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public float inputVector { get; private set; }
    private int ButtonID = 0;

    [SerializeField] private float Sensetivity = 10.0f;

    private void FixedUpdate()
    {
        switch (ButtonID)
        {
            case -1:
                {
                    inputVector -= Sensetivity;
                    inputVector = Mathf.Clamp(inputVector, -1, 2);
                    break;
                }
            case 1:
                {
                    inputVector += Sensetivity;
                    inputVector = Mathf.Clamp(inputVector, -1, 2);
                    break;
                }
            default:
                {
                    inputVector = Mathf.Lerp(inputVector, 0, 0.5f);
                    break;
                }
        }
    }

    public void ButtonClick(int buttonID)
    {
        ButtonID = buttonID;
    }
}
