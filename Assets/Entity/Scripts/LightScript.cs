using System;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public Light light;
    public GameObject parent;
    public bool playing;
    public bool unPlaying;

    public float maxRange = 5;


    private void Start()
    {
        light = GetComponent<Light>();
    }

    private void Update()
    {
        if (playing)
        {
            Interpolate();
        }

        if (unPlaying)
        {
            
            DeInterpolate();
        }
        
        if (Mathf.Approximately(light.range, 0))
        {
            unPlaying = false;

            if (parent != null)
            {
                transform.parent = parent.transform;
                transform.localPosition=Vector3.zero;
                light.intensity = 0;
                light.range = 0;
                return;
            }
            Destroy(gameObject);
        }
        
    }

    public void Trigger(Vector3 pos)
    {
        transform.parent = null;
        transform.position = pos;
        unPlaying = false;
        playing = true;
        light.intensity = 0;
        light.range = 0;
        Interpolate();

    }

    public void ChangeColor(Color color)
    {
        light.color = color;
    }

    private void Interpolate()
    {
        
        light.intensity = Mathf.Lerp(light.intensity,2,0.1f);
        light.range = Mathf.Lerp(light.range,maxRange,0.1f);

        if (Mathf.Approximately(light.range, maxRange))
        {
            
           
            playing = false;
            unPlaying = true;



        }

    }

    private void DeInterpolate()
    {
        light.intensity = Mathf.Lerp(light.intensity,0,0.1f);
        light.range = Mathf.Lerp(light.range,0,0.1f);

        if (light.range<0.1f)
        {
            
            
            unPlaying = false;

            if (parent != null)
            {
                transform.parent = parent.transform;
                transform.localPosition=Vector3.zero;
                light.intensity = 0;
                light.range = 0;
                return;
            }
            Destroy(gameObject);
        }
    }
}
