using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCollectible : MonoBehaviour, ICollectible
{
    public abstract CollectibleType collectibleType { get; }

    public virtual void GetCollected()
    {
        EventManager._onCollected(collectibleType);
    }

    private void OnTriggerEnter(Collider other)
    {
        GetCollected();
    }

    //AnimEvent ile kullanılıyor.
    private void DestroyObj()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        //Destroy(gameObject);
    }
}
