using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{    
    [HideInInspector] public AsyncOperation asyncLoad = null;
    [HideInInspector] public AsyncOperation asyncUnload = null;
    [Header("Progress Bar")]
    public Texture2D emptyProgressBar;
    public Texture2D fillProgressBar;
    public bool showProgress = false;

    //simple async scene load
    public void LoadScene(int sceneNum)
    {
        //Set timescale to 1 just in case, otherwise scene loading wont work correct
        //Besides, when a new scene is loading we probably dont want the timescale to be zero (game paused)
        Time.timeScale = 1;
        StartCoroutine(Load(sceneNum));
        StartCoroutine(Progress());
    }

    //async unload a scene and then async load other scene 
    public void LoadSceneUnloadOld(int unloadSceneNum, int NewSceneNum)
    {
        Time.timeScale = 1;
        StartCoroutine(UnloadLoad(unloadSceneNum, NewSceneNum));
        StartCoroutine(Progress());
    }

    //async load a scene additive 
    public void LoadSceneAdditive(int sceneNum)
    { 
        Time.timeScale = 1;
        StartCoroutine(LoadAdditive(sceneNum));
        StartCoroutine(Progress());
    }

    //simple async unload scene
    public void UnLoadScene(int sceneNum)
    {
        Time.timeScale = 1;
        StartCoroutine(UnLoad(sceneNum));
        StartCoroutine(Progress());
    }

    //Set a scene as active
    public void SetActiveScene(int sceneNum)
    {
        Time.timeScale = 1;
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneNum));
    }
 
    //////////////////////////////////////////////////////

    //loads a scene -use LoadScene()
    IEnumerator Load(int sceneNum)
    {
        //yield return new WaitForSecondsRealtime(0.5f);  
        asyncLoad = SceneManager.LoadSceneAsync(sceneNum, LoadSceneMode.Single);
        yield return null;
    }

    //Unloads a scene and loads an other -use LoadSceneUnloadOld()
    IEnumerator UnloadLoad(int unloadSceneNum, int NewSceneNum)
    {
        asyncUnload = SceneManager.UnloadSceneAsync(unloadSceneNum);
        asyncLoad = SceneManager.LoadSceneAsync(NewSceneNum, LoadSceneMode.Single);
        yield return null;
    }

    //loads a scene additive -use LoadSceneAdditive()
    IEnumerator LoadAdditive(int sceneNum)
    {
        asyncLoad = SceneManager.LoadSceneAsync(sceneNum, LoadSceneMode.Additive);
        yield return null;
    }

    //Unloads a scene -use UnLoadScene()
    IEnumerator UnLoad(int sceneNum)
    {        
        asyncUnload = SceneManager.UnloadSceneAsync(sceneNum);
        yield return null;
    }

    //report back the asyncLoad stats for the progress bar
    IEnumerator Progress()
    {
        yield return asyncLoad;
    }

    //Shows a loading bar style... thing with the actual progress    
    void OnGUI()
    {
        //Lets check if there is data loading asynchronous
        if (showProgress && asyncLoad != null && asyncLoad.progress * 100 != 100)
        {
            //Draw the Progress Bar
            GUI.DrawTexture(new Rect(Screen.width / 2 - 400 / 2, Screen.height - 50, 400, 50), emptyProgressBar);
            GUI.DrawTexture(new Rect(Screen.width / 2 - 400 / 2, Screen.height - 50, 400 * asyncLoad.progress, 50), fillProgressBar);
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUI.Label(new Rect(Screen.width / 2 - 400 / 2, Screen.height - 50, 400, 50), string.Format("{0:N0}%", asyncLoad.progress * 100f));
        }
    }
 }
