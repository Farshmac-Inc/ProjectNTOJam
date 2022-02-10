using UnityEngine;
using UnityEngine.Events;

public class Wheel : MonoBehaviour
{
    public UnityEvent<Collision2D> CollisionEnter;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionEnter?.Invoke(collision);
    }

}
