using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRoof : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponentInParent<PlayerController>().Death();
    }
}
