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

    public Text coinText;
    public Text healthText;
    public Text shieldText;

    public int maxCoinLVL;
    public int maxHealthLVL;
    public int maxShieldLVL;

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
    }

    private void Update()
    {
        if (isOnSplash && Input.touchCount > 0)
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
        if (PlayerPrefs.GetInt("CoinLevel") < maxCoinLVL)
        {
            PlayerPrefs.SetInt("CoinLevel", PlayerPrefs.GetInt("CoinLevel") + 1);
            coinText.text = "Coin Level " + PlayerPrefs.GetInt("CoinLevel");
        }
    }

    public void BTN_UpgradeHealth()
    {
        if (PlayerPrefs.GetInt("HealthLevel") < maxHealthLVL)
        {
            PlayerPrefs.SetInt("HealthLevel", PlayerPrefs.GetInt("HealthLevel") + 1);
            healthText.text = "Health Level " + PlayerPrefs.GetInt("HealthLevel");
        }
    }

    public void BTN_UpgradeShield()
    {
        if (PlayerPrefs.GetInt("HealthLevel") < maxShieldLVL)
        {
            PlayerPrefs.SetInt("HealthLevel", PlayerPrefs.GetInt("HealthLevel") + 1);
            healthText.text = "Health Level " + PlayerPrefs.GetInt("HealthLevel");
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
