using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class Obstacle : NeedStopObject
{
    [SerializeField] private Sprite RemovedObstacle;
    public override void Result()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponentsInChildren<Collider2D>()[1].enabled = false;
        GetComponent<SpriteRenderer>().sprite = RemovedObstacle;
    }
}
