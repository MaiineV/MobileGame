using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTranslate : MonoBehaviour
{
    [SerializeField] string _ID;

    [SerializeField] LangManager _manager;

    [SerializeField] Text _myView;

    void Awake()
    {
        _manager.onUpdate += ChangeLang;
    }

    void ChangeLang()
    {
        _myView.text = _manager.GetTranslate(_ID);
        Debug.Log("cambie?");
    }
}
