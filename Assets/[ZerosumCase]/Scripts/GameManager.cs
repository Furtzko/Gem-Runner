using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : BaseSingleton<GameManager>
{
    public GameState currentState;

    public int CurrencyAmount;
    public int StackAmountOnStart;
    public int CurrentStackAmount;
    public int MaxStackAmount = 20;
    public float CurrentStackPercentage;
    public bool isStackFull = false;
    public int UpgradePrice;

    private int HighScore;
    public int CurrentLevel;

    void Awake()
    {
        EventManager.OnStateChanged += GameStateChanged;
        EventManager.OnUseCurrency += CurrencyUsed;
        EventManager.OnCollected += CollectibleCollected;
        EventManager.OnHitObstacle += HitObstacle;
    }

    private void OnDestroy()
    {
        EventManager.OnStateChanged -= GameStateChanged;
        EventManager.OnUseCurrency -= CurrencyUsed;
        EventManager.OnCollected -= CollectibleCollected;
        EventManager.OnHitObstacle -= HitObstacle;
    }

    private void Start()
    {
        UpdateGameState(GameState.TapToPlay);
    }

    public void UpdateGameState(GameState newState)
    {
        currentState = newState;
        EventManager._onStateChanged(newState);
    }

    private void GameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.TapToPlay:
                InitLevel();
                break;
            case GameState.InGame:
                break;
            case GameState.LevelEnd:
                SaveLevelEndMetrics();
                break;
            default:
                break;
        }
    }

    private void InitLevel()
    {
        CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
        if (CurrentLevel == 0)
        {
            CurrentLevel++;
            PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
        }

        CurrencyAmount = PlayerPrefs.GetInt("CurrencyAmount");
        
        StackAmountOnStart = PlayerPrefs.GetInt("StartStackAmount");
        CurrentStackAmount = StackAmountOnStart;

        UpdateStackValueBy(0);


        UpgradePrice = PlayerPrefs.GetInt("UpgradePrice");
        if(UpgradePrice == 0)
        {
            UpgradePrice = 5;
            PlayerPrefs.SetInt("UpgradePrice", UpgradePrice);
        }
    }

    private void SaveLevelEndMetrics()
    {
        CurrentLevel++;
        PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
        PlayerPrefs.SetInt("CurrencyAmount", CurrencyAmount);
        PlayerPrefs.SetInt("StartStackAmount", 0);
        PlayerPrefs.SetInt("UpgradePrice", 5);
    }

    //Mevcut stack değerini value kadar artırır ve stack bar UI'ını günceller.
    public void UpdateStackValueBy(int value)
    {
        if((value>0 && !isStackFull) || (value<0 && CurrentStackAmount != 0) || value == 0)
        {
            CurrentStackAmount += value;
            CurrentStackPercentage = (float)CurrentStackAmount / (float)MaxStackAmount;

            EventManager._onStackValueChanged(CurrentStackPercentage);

            if (CurrentStackAmount.Equals(MaxStackAmount) && !isStackFull)
            {
                EventManager._onFullStack();
                isStackFull = true;
            }
            else if (!CurrentStackAmount.Equals(MaxStackAmount) && isStackFull)
            {
                EventManager._onFullStackOver();
                isStackFull = false;
            }
        }
        
    }

    //TapToStart ekranında currency kullanıldığında ilgili değerleri günceller ve PlayerPref'leri kaydeder.
    private void CurrencyUsed()
    {
        CurrencyAmount -= UpgradePrice;
        PlayerPrefs.SetInt("CurrencyAmount", CurrencyAmount);

        UpgradePrice = GetNextUpgradePrice(UpgradePrice);
        PlayerPrefs.SetInt("UpgradePrice", UpgradePrice);

        StackAmountOnStart++;
        PlayerPrefs.SetInt("StartStackAmount", StackAmountOnStart);

        UpdateStackValueBy(1);
    }

    public int GetNextUpgradePrice(int currentPrice)
    {
        int nextPrice = currentPrice * 2;
        return nextPrice;
    }

    private void CollectibleCollected(CollectibleType type)
    {
        if (type.Equals(CollectibleType.Diamond))
        {
            UpdateStackValueBy(1);
        }
        else
        {
            CurrencyAmount++;
        }
    }

    private void HitObstacle()
    {
        UpdateStackValueBy(-1);
    }
}

public enum GameState
{
    TapToPlay,
    InGame,
    LevelEnd
}
