
using System.Collections.Generic;
using System.Linq;

using UnityEngine;


public class SoundHandler : MonoBehaviour
{
    public GameObject soundObjectPrefab;
    
    public List<SoundObjectScript> soundObjectScriptList = new List<SoundObjectScript>();
    public List<SoundObjectScript> toDestroySoundObjectScriptList = new List<SoundObjectScript>();
    private Dictionary<GameObject, SoundObjectScript> accessDico = new Dictionary<GameObject, SoundObjectScript>();


    
    public void CreateAudioSource(GameObject parent )
    {
        SoundObjectScript newSoundObject = Instantiate(soundObjectPrefab).GetComponent<SoundObjectScript>();
        newSoundObject.Create(parent);
        newSoundObject.Moove();
        soundObjectScriptList.Add(newSoundObject);
        accessDico.Add(parent,newSoundObject);
        
    }

    public void Play(GameObject parentKey,AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }
            
        accessDico[parentKey].Play(clip);
    }
    public void Moove(GameObject parentKey)
    {
        accessDico[parentKey].Moove();
    }

    private void Update()
    {
        if (toDestroySoundObjectScriptList.Count > 0)
        {
            toDestroySoundObjectScriptList.ForEach(x=>Kill(x));
        }
        
    }

    public void Kill(SoundObjectScript objectScript,GameObject key=null)
    {
        
        if (key is not null)
        {
            
            objectScript = accessDico[key];
        }

        if (objectScript == null) return;
        
        if (!objectScript.thisAudioSource.isPlaying && objectScript.parent == null)
        {
            if (!toDestroySoundObjectScriptList.Contains(objectScript))
            {
                toDestroySoundObjectScriptList.Add(objectScript);
            }
            
            return ;
        }


        if (key is not null)
        {
            accessDico.Remove(key);
        }
        else
        {
            var item = accessDico.First(kvp => kvp.Value == objectScript);

            accessDico.Remove(item.Key); 
        }
        
        soundObjectScriptList.Remove(objectScript);
        
       objectScript.Kill();
        
    }

    public bool CheckByValue(SoundObjectScript soundObjectScript)
    {
        return accessDico.ContainsValue(soundObjectScript);

    }
    
    public bool CheckByKey(GameObject key)
    {
        return accessDico.ContainsKey(key);

    }

}
