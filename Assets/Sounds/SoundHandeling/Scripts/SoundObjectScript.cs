using System;
using UnityEngine;

public class SoundObjectScript : MonoBehaviour
{
    public GameObject parent;
    public AudioSource thisAudioSource;


    public void Create(GameObject toBeParent)
    {
        parent = toBeParent;
    }

    public void Play(AudioClip clip)
    {
        thisAudioSource.PlayOneShot(clip);
    }

    public void Moove()
    {
        gameObject.transform.position = parent.transform.position;
    }
    public void Kill()
    {
        Destroy(gameObject);
    }
}
