using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMangerScript : MonoBehaviour
{

    AsyncOperation asyncLoad;
    bool bLoadDone;
   
    
  
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
            

    }


    public void Load(titleButton tbutton)
    {
        int toCarry = tbutton switch
        {
            titleButton.Title => 0,
            titleButton.Play1 => 1,
            titleButton.PlayRigged => 2,
            titleButton.PlayFull => 3,
            titleButton.Reset => SceneManager.GetActiveScene().buildIndex,
            _ => throw new ArgumentOutOfRangeException(nameof(tbutton), tbutton, null)
        };

        StartCoroutine(LoadAsyncScene(toCarry));

    }
    
   private IEnumerator LoadAsyncScene(int targetScene)
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

    private void Update()
    {
        // if (Input.GetKeyUp(KeyCode.A))//&
        // {
        //     Load(titleButton.Play1);
        // }
        // if (Input.GetKeyUp(KeyCode.Z))
        // {
        //     Load(titleButton.PlayFull);
        // }
        // if (Input.GetKeyUp(KeyCode.E))
        // {
        //     Load(titleButton.PlayRigged);
        // }
        if (Input.GetKeyUp(KeyCode.R))
        {
            Load(titleButton.Reset);
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            Load(titleButton.Title);
        }
        
        
    }

}

public enum titleButton
{
    Title,
    Play1,
    PlayRigged,
    PlayFull,
    Reset
}
