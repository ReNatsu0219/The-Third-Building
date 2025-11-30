using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private List<Transform> hingePosList;

    void Awake()
    {
        hingePosList = transform.GetComponentsInChildren<Transform>().ToList();
        hingePosList.RemoveAt(0);
    }

    public static Vector3 Bezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 temp;
        Vector3 p0p1top2 = (1 - t) * p0 + t * p1;
        Vector3 p1p2top3 = (1 - t) * p1 + t * p2;
        Vector3 p2p3 = (1 - t) * p2 + t * p3;
        p0p1top2 = (1 - t) * p0p1top2 + t * p1p2top3;
        p1p2top3 = (1 - t) * p1p2top3 + t * p2p3;
        temp = (1 - t) * p0p1top2 + t * p1p2top3;
        return temp;
    }
    //private Vector3 GetControlPoint()
}
