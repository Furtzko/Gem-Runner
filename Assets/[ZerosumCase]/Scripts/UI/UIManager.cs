using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject TapToPlayUI;
    [SerializeField] private GameObject InGameUI;
    [SerializeField] private GameObject LevelEndUI;

    private void Awake()
    {
        EventManager.OnStateChanged += GameStateChanged;
    }

    private void OnDestroy()
    {
        EventManager.OnStateChanged -= GameStateChanged;
    }

    private void GameStateChanged(GameState state)
    {
        TapToPlayUI.SetActive(state == GameState.TapToPlay);
        InGameUI.SetActive(state == GameState.InGame);
        LevelEndUI.SetActive(state == GameState.LevelEnd);
    }
}
