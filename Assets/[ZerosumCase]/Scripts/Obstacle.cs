using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        EventManager._onHitObstacle();
        animator.SetTrigger("goDown");
    }

    //AnimationEvent
    //public void DestroyObj()
    //{
    //    Destroy(gameObject);
    //}
}
