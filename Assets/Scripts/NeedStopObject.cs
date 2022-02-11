using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class NeedStopObject : MonoBehaviour
{
    [SerializeField] private UnityEvent<int> UpdateUI;
    [SerializeField] private UnityEvent<Vector3> InstallSlider;
    [SerializeField] private UnityEvent RemoveSlider;
    [Range(0.001f, 1f)] [SerializeField] private float CoroutineTimer = 0.1f;

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
            coroutine = null;
        }
    }

    IEnumerator UploadingCoroutine()
    {
        while (value > 0)
        {
            UpdateUI?.Invoke((int)(defaultValue - value));
            yield return new WaitForSeconds(CoroutineTimer);
            value--;
            VisualEffect();
        }
        RemoveSlider?.Invoke();
        Result();
        countryAccepted = true;
        GetComponent<Collider2D>().enabled = false;
        StopCoroutine(coroutine);
    }

    public virtual void Result()
    {

    }

    public virtual void VisualEffect()
    {

    }
}
