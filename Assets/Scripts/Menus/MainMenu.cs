using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject secretMenu;
    public GameObject tutorialMenu;
    public GameObject creditsMenu;
    public GameObject shopMenu;
    public GameObject mainScreen;
    public GameObject splashScreen;

    public Toggle musicToggle;
    public Toggle soundToggle;

    public Text coinLVLText;
    public Text healthLVLText;
    public Text shieldLVLText;
    public Text ammountCoins;

    public int maxCoinLVL;
    public int maxHealthLVL;
    public int maxShieldLVL;

    public int coinCost;
    public int healthCost;
    public int shieldCost;

    GameObject actualMenu;
    public bool isOnSplash = true;

    private void Start()
    {
        SoundManager.instance.ChangeVolumeMusic(PlayerPrefs.GetInt("Music"));
        if (PlayerPrefs.GetInt("Music") == 0)
            musicToggle.isOn = false;
        else
            musicToggle.isOn = true;

        SoundManager.instance.ChangeVolumeSound(PlayerPrefs.GetInt("Sound"));
        if (PlayerPrefs.GetInt("Sound") == 0)
            soundToggle.isOn = false;
        else
            soundToggle.isOn = true;

        ammountCoins.text = "Coins: " + PlayerPrefs.GetInt("Coins");
        coinLVLText.text = "Coin Level " + PlayerPrefs.GetInt("CoinLevel");
        healthLVLText.text = "Health Level " + PlayerPrefs.GetInt("HealthLevel");
        shieldLVLText.text = "Shield Level " + PlayerPrefs.GetInt("ShieldLevel");
    }

    private void Update()
    {
        if (isOnSplash && (Input.touchCount > 0 || Input.anyKey))
        {
            splashScreen.SetActive(false);
            mainScreen.SetActive(true);
            isOnSplash = false;
        }
    }

    public void BTN_Start()
    {
        SceneManager.LoadScene("Level1");
    }

    public void BTN_Shop()
    {
        actualMenu = shopMenu;
        mainScreen.SetActive(false);
        shopMenu.SetActive(true);
    }

    public void BTN_UpgradeCoin()
    {
        if (PlayerPrefs.GetInt("CoinLevel") < maxCoinLVL && CoinManager.instance.CheckCost(coinCost))
        {
            PlayerPrefs.SetInt("CoinLevel", PlayerPrefs.GetInt("CoinLevel") + 1);
            coinLVLText.text = "Coin Level " + PlayerPrefs.GetInt("CoinLevel");
            ammountCoins.text = "Coins: " + PlayerPrefs.GetInt("Coins");
        }
    }

    public void BTN_UpgradeHealth()
    {
        if (PlayerPrefs.GetInt("HealthLevel") < maxHealthLVL && CoinManager.instance.CheckCost(healthCost))
        {
            PlayerPrefs.SetInt("HealthLevel", PlayerPrefs.GetInt("HealthLevel") + 1);
            healthLVLText.text = "Health Level " + PlayerPrefs.GetInt("HealthLevel");
            ammountCoins.text = "Coins: " + PlayerPrefs.GetInt("Coins");
        }
    }

    public void BTN_UpgradeShield()
    {
        if (PlayerPrefs.GetInt("ShieldLevel") < maxShieldLVL && CoinManager.instance.CheckCost(shieldCost))
        {
            PlayerPrefs.SetInt("ShieldLevel", PlayerPrefs.GetInt("ShieldLevel") + 1);
            shieldLVLText.text = "Shield Level " + PlayerPrefs.GetInt("ShieldLevel");
            ammountCoins.text = "Coins: " + PlayerPrefs.GetInt("Coins");
        }
    }

    public void BTN_Credits()
    {
        actualMenu = creditsMenu;
        mainScreen.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void BTN_Options()
    {
        actualMenu = secretMenu;
        mainScreen.SetActive(false);
        secretMenu.SetActive(true);
    }

    public void SetMusicState(bool state)
    {
        if (state)
        {
            SoundManager.instance.ChangeVolumeMusic(1);
            PlayerPrefs.SetInt("Music", 1);
        }
        else
        {
            SoundManager.instance.ChangeVolumeMusic(0);
            PlayerPrefs.SetInt("Music", 0);
        }
    }

    public void SetSoundsState(bool state)
    {
        if (state)
        {
            SoundManager.instance.ChangeVolumeSound(1);
            PlayerPrefs.SetInt("Sound", 1);
        }
        else
        {
            SoundManager.instance.ChangeVolumeSound(0);
            PlayerPrefs.SetInt("Sound", 0);
        }
    }

    public void SetButtons(int button)
    {
        PlayerPrefs.SetInt("ActualControls", button);
    }

    public void BTN_Tutorial()
    {
        actualMenu = tutorialMenu;
        mainScreen.SetActive(false);
        tutorialMenu.SetActive(true);
    }

    public void BTN_Back()
    {
        actualMenu.SetActive(false);
        mainScreen.SetActive(true);
    }
}
