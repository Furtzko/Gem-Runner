using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StackBar : MonoBehaviour
{
    [SerializeField] private Image foregroundImage;
    [SerializeField] private float updateSpeed = 0.2f;
    [SerializeField] private ParticleSystem glowParticle;
    private Animator animator;

    private void Awake()
    {
        EventManager.OnStackValueChanged += HandleStackBar;
        EventManager.OnStateChanged += GameStateChanged;
    }

    private void OnDestroy()
    {
        EventManager.OnStackValueChanged -= HandleStackBar;
        EventManager.OnStateChanged -= GameStateChanged;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void HandleStackBar(float percentage)
    {
        StartCoroutine(ChangeToPercentage(percentage));
    }

    private IEnumerator ChangeToPercentage(float percentage)
    {
        float preChangePercentage = foregroundImage.fillAmount;
        float elapsed = 0f;

        while (elapsed < updateSpeed)
        {
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangePercentage, percentage, elapsed/updateSpeed);
            yield return null;
        }

        foregroundImage.fillAmount = percentage;

        if (GameManager.Instance.isStackFull)
        {
            glowParticle.Play();
            animator.SetBool("isStackFull", true);
        }
        else
        {
            glowParticle.Stop();
            animator.SetBool("isStackFull", false);
        }
    }

    private void GameStateChanged(GameState state)
    {
        gameObject.SetActive(state != GameState.LevelEnd);
    }
}
