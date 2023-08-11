using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager instance;
    
    private bool _isLoading = false;
    [SerializeField] private GameObject _loadingCanvas;
    [SerializeField] private Image _loadingBar;
    [SerializeField] private GameObject _loadingLogo;

    private List<int> scenesLoaded = new List<int>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;
        
        LoadScene(1);
    }

    public void LoadScene(int sceneToLoad)
    {
        if (_isLoading) return;

        _loadingCanvas.SetActive(true);
        _loadingBar.fillAmount = 0;

        _isLoading = true;
        StartCoroutine(LoadCoroutine(sceneToLoad));
    }

    private IEnumerator LoadCoroutine(int index)
    {
        foreach (var loadedIndex in scenesLoaded)
        {
            var scene = SceneManager.GetSceneByBuildIndex(loadedIndex);

            if (!scene.isLoaded) continue;
            
            var unloadSceneAsync = SceneManager.UnloadSceneAsync(scene, UnloadSceneOptions.None);
            
            if (unloadSceneAsync != null)
                yield return new WaitUntil(() => unloadSceneAsync.isDone);
        }

        scenesLoaded = new List<int>();
        scenesLoaded.Add(index);

        var asyncOp = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);

        yield return new WaitUntil(() => asyncOp.isDone);

        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(index));

        _loadingCanvas.SetActive(false);
        _isLoading = false;
    }
}