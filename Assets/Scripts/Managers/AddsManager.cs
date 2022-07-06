using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AddsManager : MonoBehaviour
{
    public static AddsManager instance;
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

        Advertisement.Initialize("4826665");
    }

    public void ShowAdd()
    {
        if (!Advertisement.isInitialized) return;

        var adOptions = new ShowOptions();
        adOptions.resultCallback = AdResult;
        Advertisement.Show("Rewarded_Android", adOptions);
    }

    void AdResult(ShowResult result)
    {
        if(result == ShowResult.Failed)
        {
            EventManager.Trigger("Finish", 0);
        }
        else if (result == ShowResult.Skipped)
        {
            EventManager.Trigger("Finish", 1);
        }
        else if(result == ShowResult.Finished)
        {
            EventManager.Trigger("Finish", 2);
        }
    }
}
