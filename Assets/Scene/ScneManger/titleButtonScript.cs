
using System;
using UnityEngine;

public class titleButtonScript : MonoBehaviour
{
    public bool isButton = false;

    private SceneMangerScript sceneMangerScript;
    private SpriteRenderer spriteRenderer;

    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;

    public titleButton sceneTo;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sceneMangerScript = FindAnyObjectByType<SceneMangerScript>();
    }


    private void OnMouseUpAsButton()
    {
        if(!isButton)return;
        spriteRenderer.sprite = sprite3;
        sceneMangerScript.Load(sceneTo);
    }

    private void OnMouseEnter()
    {
        spriteRenderer.sprite = sprite2;
    }

    private void OnMouseExit()
    {
        spriteRenderer.sprite = sprite1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isButton||!other.gameObject.CompareTag("Entity"))return;
        sceneMangerScript.Load(sceneTo);
    }
}
