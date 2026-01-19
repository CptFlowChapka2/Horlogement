using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMangerScript : MonoBehaviour
{

    AsyncOperation asyncLoad;
    bool bLoadDone;
    public SceneAsset titleMenu;
    private string titleMenuString;
    
    public SceneAsset playScene;
    private string playSceneString;
    
  
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        titleMenuString = titleMenu.name;
        playSceneString = playScene.name;

    }


    public void Load(titleButton tbutton)
    {
        string toCarry = tbutton switch
        {
            titleButton.Title => titleMenuString,
            titleButton.Play1 => playSceneString,
            _ => throw new ArgumentOutOfRangeException(nameof(tbutton), tbutton, null)
        };
        
        SceneManager.LoadSceneAsync(toCarry);
    }
    
    IEnumerator LoadAsyncScene(string targetScene)
    {
        asyncLoad = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false;
        //wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            //scene has loaded as much as possible,
            // the last 10% can't be multi-threaded
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
        bLoadDone = asyncLoad.isDone;
    }

}

public enum titleButton
{
    Title,
    Play1
}
