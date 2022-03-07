using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 offset;
    private Vector3 newPosition;
    [SerializeField] private float smoothTime = 0.8f;

    void Start()
    {
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        newPosition = new Vector3(offset.x + target.position.x, transform.position.y, offset.z + target.position.z);
        transform.position = Vector3.Slerp(transform.position, newPosition, smoothTime * Time.deltaTime);
    }
}
