using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public Transform[] points;

    public Transform RandomPoint()
    {
        Transform randomPoint = points[Random.Range(0, points.Length)];
        return randomPoint;
    }
}
