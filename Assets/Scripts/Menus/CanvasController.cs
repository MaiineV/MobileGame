using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public List<GameObject> lifes;
    public GameObject loseScreen;
    public GameObject initialScreen;
    public GameObject pauseScreen;
    public GameObject baseScreen;

    public Text scoreText;
    public float score;

    public Text scoreLoseText;

    void Start()
    {
        EventManager.Subscribe("DmgLife", DmgLife);
        EventManager.Subscribe("HealLife", HealLife);
    }

    private void Update()
    {
        score += Time.deltaTime;
        scoreText.text = "Score: " + (int)score;
    }

    public void BTN_Start()
    {
        initialScreen.gameObject.SetActive(false);
        baseScreen.gameObject.SetActive(true);
        EventManager.Trigger("ChangeBool");
    }

    public void BTN_Pause()
    {
        baseScreen.gameObject.SetActive(false);
        pauseScreen.gameObject.SetActive(true);
        EventManager.Trigger("ChangeBool");
    }

    public void BTN_StopPause()
    {
        baseScreen.gameObject.SetActive(true);
        pauseScreen.gameObject.SetActive(false);
        EventManager.Trigger("ChangeBool");
    }

    public void BTN_Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void BTN_Retry()
    {
        SceneManager.LoadScene("Level1");
    }

    void DmgLife(params object[] parameter)
    {
        lifes[(int)parameter[0]].SetActive(false);
        Debug.Log(parameter[0]);

        if((int)parameter[0] <= 0)
        {
            scoreLoseText.text = "Score: " + (int)score;
            loseScreen.SetActive(true);
            baseScreen.gameObject.SetActive(false);
        }
    }

    void HealLife(params object[] parameter)
    {
        lifes[(int)parameter[0]].SetActive(true);
    }
}
