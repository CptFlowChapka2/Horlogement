using System;
using UnityEngine;

public class titleButtonScript : MonoBehaviour
{

    private SceneMangerScript sceneMangerScript;

    public titleButton sceneTo;

    private void Start()
    {
        sceneMangerScript = FindAnyObjectByType<SceneMangerScript>();
    }


    private void OnMouseUpAsButton()
    {
        sceneMangerScript.Load(sceneTo);
    }
}
