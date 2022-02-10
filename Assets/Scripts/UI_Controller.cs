using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    public Canvas Canvas;
    public GameObject UploadingSliderObj;
    public Text Speedometr;

    private Slider UploadingSlider;
    private void Start()
    {
        UploadingSlider = UploadingSliderObj.GetComponent<Slider>();
    }
    public void InstallUploadSlider(Vector3 position)
    {
        UploadingSliderObj.transform.position = Camera.main.WorldToScreenPoint(position);
        UploadingSliderObj.SetActive(true);
        UploadingSlider.value = 0;
    }

    public void RemoveUploadSlider()
    {
        UploadingSliderObj.SetActive(false);
    }

    public void SetValueUploadSlider (int value)
    {
        UploadingSlider.value = value;
    }

    public void SetSpeedValue (string value)
    {
        Speedometr.text = value;
    }
}
