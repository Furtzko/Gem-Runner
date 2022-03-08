using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem sparks;

    private void OnTriggerEnter(Collider other)
    {
        if(GameManager.Instance.CurrentStackAmount != 0)
        {
            sparks.transform.position = new Vector3(other.transform.position.x, sparks.transform.position.y, sparks.transform.position.z);
            sparks.Play();
        }
        
        EventManager._onHitObstacle();
        animator.SetTrigger("goDown");
    }

}
