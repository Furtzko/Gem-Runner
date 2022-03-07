using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : BaseCollectible
{
    private Animator animator;
    public override CollectibleType collectibleType => CollectibleType.Diamond;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void GetCollected()
    {
        base.GetCollected();
        //TODO: özelleştir.

        //GameManager.Instance.CurrentStackAmount++;

        //float currentPercentage = (float)GameManager.Instance.CurrentStackAmount / (float)GameManager.Instance.MaxStackAmount;

        //EventManager._onStackValueChanged(currentPercentage);

        animator.SetTrigger("isCollected");
    }

}
