using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : BaseCollectible
{
    private Animator animator;
    [SerializeField] private ParticleSystem particle;
    public override CollectibleType collectibleType => CollectibleType.Gold;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void GetCollected()
    {
        base.GetCollected();
        //TODO: özelleştir
        particle.Play();
        animator.SetTrigger("isCollected");
    }
}
