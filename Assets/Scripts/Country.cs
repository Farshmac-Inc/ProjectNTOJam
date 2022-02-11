using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Country : MonoBehaviour
{
    [SerializeField] private UnityEvent<int> UpdateUI;
    [SerializeField] private UnityEvent<Vector3> InstallSlider;
    [SerializeField] private UnityEvent RemoveSlider;

    public UnityEvent<float> UnloadingFinish;
    [SerializeField] private float NeededCargoMass = 2.0f;

    private float value = 100;
    private int defaultValue = 100;
    private bool countryAccepted = false;

    private Coroutine coroutine;

    public void UnloadingState(bool onUploading)
    {
        if (countryAccepted) return;
        if (onUploading && coroutine == null)
        {
            InstallSlider?.Invoke(transform.position);
            coroutine = StartCoroutine(UploadingCoroutine());
        }
        if (!onUploading && coroutine != null)
        {
            value = defaultValue;
            RemoveSlider?.Invoke();
            StopCoroutine(coroutine);
        }
    }

    IEnumerator UploadingCoroutine()
    {
        while (value > 0)
        {
            UpdateUI?.Invoke((int)(defaultValue - value));
            yield return new WaitForSeconds(0.1f);
            value--;
        }
        RemoveSlider?.Invoke();
        UnloadingFinish?.Invoke(NeededCargoMass);
        UnloadingFinish.RemoveAllListeners();

        countryAccepted = true;
        GetComponent<Collider2D>().enabled = false;
        StopCoroutine(coroutine);
    }
}
