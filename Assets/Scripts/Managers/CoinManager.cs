using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    int _actualCoins;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _actualCoins = PlayerPrefs.GetInt("Coins");
    }

    public void AddCoins(int coins)
    {
        _actualCoins += coins;
        PlayerPrefs.SetInt("Coins", _actualCoins);
    }

    public bool CheckCost(int cost)
    {
        if(_actualCoins >= cost)
        {
            _actualCoins -= cost;
            PlayerPrefs.SetInt("Coins", _actualCoins);
            return true;
        }
        return false;
    }
}
