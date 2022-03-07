using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StackBar : MonoBehaviour
{
    [SerializeField] private Image foregroundImage;
    [SerializeField] private float updateSpeed = 0.2f;

    private void Awake()
    {
        EventManager.OnStackValueChanged += HandleStackBar;
    }

    private void OnDestroy()
    {
        EventManager.OnStackValueChanged -= HandleStackBar;
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
    }

    private void LateUpdate()
    {
        //transform.LookAt(Camera.main.transform);
        //transform.Rotate(0, 180, 0);
    }
}
