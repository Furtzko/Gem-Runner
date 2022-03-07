using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(EndLevel());
    }

    private IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(0.5f);
        EventManager._onStateChanged(GameState.LevelEnd);
    }
}
