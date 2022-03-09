using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private GameObject coinCount;

    private void Start()
    {
        coinCount.GetComponent<TextMeshProUGUI>().text = GameManager.Instance.CurrencyAmount.ToString();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % 2);
    }
}
