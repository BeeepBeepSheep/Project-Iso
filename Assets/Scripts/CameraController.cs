using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform currentTarget; //camera controller script, makes camera follow target smoothly
    public Vector3 currentOffset;//offset
    private Vector3 velocity = Vector3.zero;
    public float followDampTime = 0.02f;
    void Update()
    {
        if (currentTarget != null)
        {
            Vector3 targetPosition = currentTarget.position + currentOffset;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, followDampTime);
        }
    }
}
