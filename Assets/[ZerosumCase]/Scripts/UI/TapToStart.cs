using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TapToStart : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject levelText;
    [SerializeField] private GameObject startStack;
    [SerializeField] private GameObject upgradePrice;
    [SerializeField] private GameObject currentCurrency;

    private void Start()
    {
        StartCoroutine(UpdateUI());

    }


    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("onpointerdown");
        EventManager._onStateChanged(GameState.InGame);
    }

    public void UseCurrency()
    {
        if(GameManager.Instance.CurrencyAmount >= GameManager.Instance.UpgradePrice)
        {
            EventManager._onUseCurrency();
            StartCoroutine(UpdateUI());
        }
    }

    private IEnumerator UpdateUI()
    {
        yield return new WaitForSeconds(0.01f);

        levelText.GetComponent<TextMeshProUGUI>().text = "LEVEL " + GameManager.Instance.CurrentLevel;

        currentCurrency.GetComponent<TextMeshProUGUI>().text = GameManager.Instance.CurrencyAmount.ToString();

        upgradePrice.GetComponent<TextMeshProUGUI>().text = GameManager.Instance.UpgradePrice.ToString();

        startStack.GetComponent<TextMeshProUGUI>().text = GameManager.Instance.StackAmountOnStart.ToString();
    }

}
