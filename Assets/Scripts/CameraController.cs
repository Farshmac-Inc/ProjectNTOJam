using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject Car;
    private Vector3 lastCarPosition = Vector3.zero;

    private void Start()
    {
        if (Car == null) Car = FindObjectOfType<PlayerController>().gameObject;
        lastCarPosition = Car.transform.position;
    }

    private void Update()
    {
        var positionOffset = Car.transform.position - lastCarPosition;
        transform.position += new Vector3(positionOffset.x, 0, 0); 
        lastCarPosition = Car.transform.position;
    }
}
