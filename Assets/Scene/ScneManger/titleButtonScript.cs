using System;
using UnityEngine;

public class titleButtonScript : MonoBehaviour
{
    public bool isButton = false;

    private SceneMangerScript sceneMangerScript;

    public titleButton sceneTo;

    private void Start()
    {
        sceneMangerScript = FindAnyObjectByType<SceneMangerScript>();
    }


    private void OnMouseUpAsButton()
    {
        if(!isButton)return;
        sceneMangerScript.Load(sceneTo);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isButton||!other.gameObject.CompareTag("Entity"))return;
        sceneMangerScript.Load(sceneTo);
    }
}
