using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] private Canvas Canvas;
    [SerializeField] private Slider UploadingSlider;
    [SerializeField] private Text Speedometr;
    [SerializeField] private Text InputText;

    private void Start()
    {
        if (Canvas == null) Canvas = FindObjectOfType<Canvas>();
        if (UploadingSlider == null) UploadingSlider = Canvas.GetComponentInChildren<Slider>();
        if (Speedometr == null) Speedometr = Canvas.GetComponentInChildren<Text>();
    }
   
    public void InstallUploadSlider(Vector3 position)
    {
        UploadingSlider.transform.position = Camera.main.WorldToScreenPoint(position);
        UploadingSlider.gameObject.SetActive(true);
        UploadingSlider.value = 0;
    }

    public void RemoveUploadSlider()
    {
        UploadingSlider.gameObject.SetActive(false);
    }

    public void SetValueUploadSlider (int value)
    {
        UploadingSlider.value = value;
    }

    public void SetSpeedValue (string value)
    {
        Speedometr.text = value;
    }

    public void SetTextValue(float value)
    {
        InputText.text = $"{value}";
    }
}
