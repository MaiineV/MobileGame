using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public enum Language
{
    eng,
    spa
}

public class LangManager : MonoBehaviour
{
    [SerializeField] Language _selectedLanguage;

    [SerializeField] string _externalURL = "https://docs.google.com/spreadsheets/d/e/2PACX-1vR-EL_l25uhAVHZ4FTpSzalus9wfFsIlrUpAt-MuXAcPJFnqoTNwCwWtw177TyYwb0amp_1iBm2CTZ7/pub?output=csv";

    Dictionary<Language, Dictionary<string, string>> _languageManager;

    public event Action onUpdate = delegate { };

    void Start()
    {
        if (PlayerPrefs.HasKey("Language"))
        {
            if(PlayerPrefs.GetInt("Language") == 0)
            {
                _selectedLanguage = Language.spa;
            }
            else
            {
                _selectedLanguage = Language.eng;
            }
        }

        StartCoroutine(DownloadCSV(_externalURL));
    }

    public void BTN_ChangeLanguage()
    {
        if (_selectedLanguage == Language.eng)
        {
            Debug.Log("Cambie a espanol");

            PlayerPrefs.SetInt("Language", 0);
            _selectedLanguage = Language.spa;
        }
        else
        {
            Debug.Log("Cambie a ingles");
            PlayerPrefs.SetInt("Language", 1);
            _selectedLanguage = Language.eng;
        }

        onUpdate();
    }

    public void ExecuteUpdate()
    {
        onUpdate();
    }

    public string GetTranslate(string id)
    {
        if (_languageManager == null)
        {
            return "There is no dictionary";
        }
        
        if (!_languageManager[_selectedLanguage].ContainsKey(id))
        {
            return "Error 404: Not Found";
        }
        else
        {
            return _languageManager[_selectedLanguage][id];
        }
    }

    IEnumerator DownloadCSV(string url)
    {
        var www = new UnityWebRequest(url);
        www.downloadHandler = new DownloadHandlerBuffer();

        yield return www.SendWebRequest();

        _languageManager = LanguageU.LoadCodexFromString("www", www.downloadHandler.text);

        onUpdate();
    }

}
