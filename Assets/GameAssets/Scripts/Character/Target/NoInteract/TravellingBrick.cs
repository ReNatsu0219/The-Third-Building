using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravellingBrick : MonoBehaviour
{
    public Vector2 posA;
    public Vector2 posB;
    public float moveSpeed;
    private Vector2 targetPos;

    void Awake()
    {
        targetPos = posA;
    }
    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, targetPos) <= moveSpeed * Time.fixedDeltaTime)
        {
            targetPos = Vector2.Distance(transform.position, posA) <= moveSpeed * Time.fixedDeltaTime ? posB : posA;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.fixedDeltaTime);
        }
    }
}
