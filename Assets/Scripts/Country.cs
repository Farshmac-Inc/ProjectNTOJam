using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Country : MonoBehaviour
{
    public UnityEvent<int> UpdateUI;
    public UnityEvent<Vector3> InstallSlider;
    public UnityEvent RemoveSlider;

    private float value = 100;
    private int defaultValue = 100;

    private PlayerController player;
    private Coroutine coroutine;

    private bool CountryAccepted = false;

    private void Start() { player = FindObjectOfType<PlayerController>(); }

    public void UploadingState(bool onUploading, PlayerController player)
    {
        if (!CountryAccepted)
        {
            if (onUploading && coroutine == null)
            {
                InstallSlider?.Invoke(transform.position);
                coroutine = StartCoroutine(UploadingCoroutine());
            }
            if (!onUploading & coroutine != null)
            {
                StopCoroutine(coroutine);
                value = defaultValue;
                RemoveSlider?.Invoke();
            }
        }
    }

    IEnumerator UploadingCoroutine()
    {
        for(; value > 0; value--)
        {
            UpdateUI?.Invoke((int)(defaultValue - value));
            yield return new WaitForSeconds(0.1f);
        }
        RemoveSlider?.Invoke();
        CountryAccepted = true;
        StopCoroutine(coroutine);
        GetComponent<Collider2D>().enabled = false;
    }
}
