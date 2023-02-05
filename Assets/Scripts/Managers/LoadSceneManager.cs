using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : SingletonSingleScene<LoadSceneManager>
{
    const string restartKeyWord = "restart";

    public static float currentProgress {get; private set;} = 1;

    public void LoadScene(string sceneName)
    {
        GameManager.Instance.Resume();
        if(sceneName == restartKeyWord) sceneName = SceneManager.GetActiveScene().name;
        
        if(currentProgress >= 1){
            StartCoroutine(LoadSceneAsync(
                sceneName,
                (float progress)=> currentProgress = progress,
                ()=> currentProgress = 1
            ));
        }
    }

    public void LoadScene(int sceneIndex)
    {
        GameManager.Instance.Resume();
        if(currentProgress >= 1){
            StartCoroutine(LoadSceneAsync(
                sceneIndex,
                (float progress)=> currentProgress = progress,
                ()=> currentProgress = 1
            ));
        }
    }

    #region LoadScene
/// <summary>
/// Load the scene with a given name with the option for Actions to get the Progress
/// as well as a method for when it has Completed.
/// </summary>
 
    public static IEnumerator LoadSceneAsync(string sceneName, Action<float> OnProgress, Action OnComplete)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
    
        while ((operation.progress / 0.9f) < 1f)
        {
            OnProgress(operation.progress / 0.9f);
            yield return null;
        }
    
        OnComplete();
        operation.allowSceneActivation = true;
    }

    public static IEnumerator LoadSceneAsync(int sceneIndex, Action<float> OnProgress, Action OnComplete)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;
    
        while ((operation.progress / 0.9f) < 1f)
        {
            OnProgress(operation.progress / 0.9f);
            yield return null;
        }
    
        OnComplete();
        operation.allowSceneActivation = true;
    }
    
    public static IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = true;
    
        while ((operation.progress / 0.9f) < 1f)
        {
            yield return null;
        }
    }

    public static IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = true;
    
        while ((operation.progress / 0.9f) < 1f)
        {
            yield return null;
        }
    }
    #endregion
}
