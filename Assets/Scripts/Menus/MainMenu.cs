using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject secretMenu;
    public GameObject tutorialMenu;
    public GameObject creditsMenu;
    public GameObject mainScreen;

    GameObject actualMenu;
    
    public void BTN_Start()
    {
        SceneManager.LoadScene("Level1");
    }

    public void BTN_Credits()
    {
        actualMenu = creditsMenu;
        mainScreen.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void BTN_Secret()
    {
        actualMenu = secretMenu;
        mainScreen.SetActive(false);
        secretMenu.SetActive(true);
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
