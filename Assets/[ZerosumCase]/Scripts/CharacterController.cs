using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private SwervingController characterMovement;
    private Animator animator;

    private void Awake()
    {
        EventManager.OnStateChanged += GameStateChanged;
        EventManager.OnFullStack += OnStackIsFull;
        EventManager.OnFullStackOver += OnStackIsNotFull;
    }

    private void OnDestroy()
    {
        EventManager.OnStateChanged -= GameStateChanged;
        EventManager.OnFullStack -= OnStackIsFull;
        EventManager.OnFullStackOver -= OnStackIsNotFull;
    }

    void Start()
    {
        characterMovement = GetComponent<SwervingController>();
        animator = GetComponent<Animator>();
    }

    private void GameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.InGame:
                characterMovement.enabled = true;
                animator.SetBool("isRunning", true);

                break;
            case GameState.LevelEnd:
                characterMovement.enabled = false;
                animator.SetTrigger("Dance");
                break;
            default:
                break;
        }
    }

    private void OnStackIsFull()
    {
        animator.SetBool("isStackFull", true);
    }

    private void OnStackIsNotFull()
    {
        animator.SetBool("isStackFull", false);
    }

}
