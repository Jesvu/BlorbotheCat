using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.2f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private Transform target;

    void Update()
    {
        // i just stole this from a tutorial lol
        Vector3 targetPosition = new Vector3(target.position.x, 0f, -10f) + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
