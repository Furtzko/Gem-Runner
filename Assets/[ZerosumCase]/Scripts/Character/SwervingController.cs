using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwervingController : MonoBehaviour
{
    private SwerveInput _swerveInput;
    [SerializeField] private float swerveSpeed = 0.5f;
    [SerializeField] private float maxSwerveAmount = 1f;
    [SerializeField] private float forwardSpeed = 5f;

    private void Awake()
    {
        EventManager.OnFullStack += OnStackIsFull;
        EventManager.OnFullStackOver += OnStackIsNotFull;
    }

    private void OnDestroy()
    {
        EventManager.OnFullStack -= OnStackIsFull;
        EventManager.OnFullStackOver -= OnStackIsNotFull;
    }

    private void Start()
    {
        _swerveInput = GetComponent<SwerveInput>();
    }

    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -1.5f, 1.5f), transform.position.y, transform.position.z);

        float swerveAmount = Time.deltaTime * swerveSpeed * _swerveInput.MoveFactorX;
        swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
        transform.Translate(swerveAmount, 0, forwardSpeed * Time.deltaTime);
    }

    private void OnStackIsFull()
    {
        forwardSpeed = 8f;
    }

    private void OnStackIsNotFull()
    {
        forwardSpeed = 5f;
    }
}
