using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] private Canvas Canvas;
    [SerializeField] private Slider UploadingSlider;
    [SerializeField] private Text Speedometr;
    [SerializeField] private GameObject Panel;
    [SerializeField] private Text ResultText;
    [SerializeField] private Text ScoreText;

    [SerializeField] private Sprite[] EndBGSprites = new Sprite[3];

    private void Start()
    {
        if (Canvas == null) Canvas = FindObjectOfType<Canvas>();
        if (UploadingSlider == null) UploadingSlider = Canvas.GetComponentInChildren<Slider>();
        if (Speedometr == null) Speedometr = Canvas.GetComponentInChildren<Text>();
    }
   
    public void InstallUploadSlider(Vector3 position)
    {
        UploadingSlider.transform.position = Camera.main.WorldToScreenPoint(position + new Vector3(0, 2f, 0));
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

    public void LVL_END(int scenario)
    {
        switch (scenario)
        {
            case 0:
                {
                    Panel.SetActive(true);
                    Time.timeScale = 0;
                    ResultText.text = "Вы спасли город!";
                    ScoreText.text = $"{1000 + Random.Range(-200, 200)}";
                    break;
                }
            case 1:
                {
                    Panel.SetActive(true);
                    Time.timeScale = 0;
                    ResultText.text = "Вы были последней надеждой города!";
                    ScoreText.text = $"{300 + Random.Range(-200, 200)}";
                    break;
                }
            case 2:
                {
                    Panel.SetActive(true);
                    Time.timeScale = 0;
                    ResultText.text = "Вы ехали слишком медленно и опоздали!";
                    ScoreText.text = $"{500 + Random.Range(-200, 200)}";
                    break;
                }
        }
    }
}
