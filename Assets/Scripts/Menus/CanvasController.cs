using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public List<GameObject> lifes;
    public GameObject loseScreen;
    public GameObject addScreen;
    public GameObject initialScreen;
    public GameObject pauseScreen;
    public GameObject optionsMenu;
    public GameObject baseScreen;

    public Text scoreText;
    public float score;

    public Text scoreLoseText;

    private bool _isPlaying = false;

    [SerializeField] private Button[] _movementButtons;
    [SerializeField] private GameObject _buttonsParent;

    private void Awake()
    {
        EventManager.Subscribe("DmgLife", DmgLife);
        EventManager.Subscribe("HealLife", HealLife);
        EventManager.Subscribe("AddScore", AddScore);
        EventManager.Subscribe("Finish", FinishRun);
        EventManager.Subscribe("SetButtons", SetButtons);
        EventManager.Subscribe("ChangeButtons", ChangeButtons);
    }

    private void Update()
    {
        if (!_isPlaying) return;
        
        score += Time.deltaTime;
        scoreText.text = "Score: " + (int)score;
    }

    public void BTN_Start()
    {
        initialScreen.gameObject.SetActive(false);
        baseScreen.gameObject.SetActive(true);
        _isPlaying = true;
        EventManager.Trigger("ChangeBool");
    }

    public void BTN_Pause()
    {
        baseScreen.gameObject.SetActive(false);
        pauseScreen.gameObject.SetActive(true);
        _isPlaying = false;
        EventManager.Trigger("ChangeBool");
    }

    public void BTN_StopPause()
    {
        baseScreen.gameObject.SetActive(true);
        pauseScreen.gameObject.SetActive(false);
        _isPlaying = true;
        EventManager.Trigger("ChangeBool");
    }

    public void BTN_Options()
    {
        pauseScreen.SetActive(false);
        optionsMenu.SetActive(true);
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

    public void BTN_Back()
    {
        optionsMenu.SetActive(false);
        pauseScreen.SetActive(true);
    }

    public void BTN_SeeAdd()
    {
        AddsManager.instance.ShowAdd();
    }

    public void BTN_SkipAdd()
    {
        CoinManager.instance.AddCoins((int)score);
        addScreen.SetActive(false);
        loseScreen.SetActive(true);
        scoreLoseText.text = "Score: " + (int)score;
    }

    public void BTN_Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void BTN_Retry()
    {
        SceneManager.LoadScene("Level1");
    }

    private void FinishRun(params object[] parameter)
    {
        int finalScore;
        if ((int)parameter[0] == 0)
            finalScore = (int)score;
        else if ((int)parameter[0] == 1)
            finalScore = (int)score + (int)((int)score * 0.25);
        else if ((int)parameter[0] == 2)
            finalScore = (int)score * 2;
        else
            finalScore = (int)score;

        CoinManager.instance.AddCoins(finalScore);
        addScreen.SetActive(false);
        loseScreen.SetActive(true);
        scoreLoseText.text = "Score: " + finalScore;
    }

    private void AddScore(params object[] parameter)
    {
        score += (int)parameter[0];
    }

    private void DmgLife(params object[] parameter)
    {
        lifes[(int)parameter[0]].SetActive(false);

        if ((int)parameter[0] <= 0)
        {
            addScreen.SetActive(true);
            baseScreen.gameObject.SetActive(false);
        }
    }

    private void HealLife(params object[] parameter)
    {
        lifes[(int)parameter[0]].SetActive(true);
    }

    private void SetButtons(params object[] parameters)
    {
        var controller = (Controller)parameters[0];

        _movementButtons[0].onClick.AddListener(controller.RightButton);
        _movementButtons[1].onClick.AddListener(controller.LeftButton);
        _movementButtons[2].onClick.AddListener(controller.JumpButton);
        _movementButtons[3].onClick.AddListener(controller.SlideButton);
    }

    private void ChangeButtons(params object[] parameters)
    {
        if ((bool)parameters[0])
        {
            _buttonsParent.SetActive(true);
        }
        else
        {
            _buttonsParent.SetActive(false);
        }
    }
}
