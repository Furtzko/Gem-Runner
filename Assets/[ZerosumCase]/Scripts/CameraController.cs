using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 offset;
    private Vector3 newPosition;
    [SerializeField] private float smoothTime = 2f;
    private bool isLevelEnd = false;
    private Quaternion qTo;

    void Awake()
    {
        EventManager.OnStateChanged += GameStateChanged;
    }

    private void OnDestroy()
    {
        EventManager.OnStateChanged -= GameStateChanged;
    }

    void Start()
    {
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        if (!isLevelEnd)
        {
            newPosition = new Vector3(offset.x + target.position.x, transform.position.y, offset.z + target.position.z);
            transform.position = Vector3.Slerp(transform.position, newPosition, smoothTime * Time.deltaTime);
        }
        else
        {
            newPosition = new Vector3(target.position.x, target.position.y + 4f, target.position.z + 5f);
            transform.position = Vector3.Slerp(transform.position, newPosition, 0.5f * Time.deltaTime);

            Quaternion OriginalRot = transform.rotation;
            transform.LookAt(target.position + Vector3.up * 2f);
            Quaternion NewRot = transform.rotation;
            transform.rotation = OriginalRot;
            transform.rotation = Quaternion.Lerp(transform.rotation, NewRot, smoothTime * Time.deltaTime);
        }
        
    }

    private void GameStateChanged(GameState state)
    {
        isLevelEnd = (state == GameState.LevelEnd);
    }
}
