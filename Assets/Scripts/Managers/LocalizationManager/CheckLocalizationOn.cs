using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLocalizationOn : MonoBehaviour
{
    [SerializeField] LangManager _manager;

    private void OnEnable()
    {
        _manager.ExecuteUpdate();
    }
}
