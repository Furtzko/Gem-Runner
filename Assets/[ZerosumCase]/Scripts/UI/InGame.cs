using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGame : MonoBehaviour
{
    [SerializeField] private GameObject levelText;
    [SerializeField] private GameObject coinCount;

    private void Awake()
    {
        EventManager.OnCollected += CollectibleCollected;
    }

    private void OnDestroy()
    {
        EventManager.OnCollected -= CollectibleCollected;
    }

    private void Start()
    {
        levelText.GetComponent<TextMeshProUGUI>().text = "LEVEL " + GameManager.Instance.CurrentLevel;

        coinCount.GetComponent<TextMeshProUGUI>().text = GameManager.Instance.CurrencyAmount.ToString();
    }

    private void CollectibleCollected(CollectibleType type)
    {
        if (type.Equals(CollectibleType.Gold))
        {
            StartCoroutine(UpdateCurrencyUI());   
        }
    }

    private IEnumerator UpdateCurrencyUI()
    {
        yield return new WaitForSeconds(0.1f);
        coinCount.GetComponent<TextMeshProUGUI>().text = GameManager.Instance.CurrencyAmount.ToString();
    }
}
