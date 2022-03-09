using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : BaseCollectible
{
    private Animator animator;
    [SerializeField] private ParticleSystem particle;
    public override CollectibleType collectibleType => CollectibleType.Diamond;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void GetCollected()
    {
        base.GetCollected();
        particle.Play();
        animator.SetTrigger("isCollected");
    }

}
