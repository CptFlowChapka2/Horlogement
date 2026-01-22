using System.Collections.Generic;
using UnityEngine;

public class MenuBackgroundScript : MonoBehaviour
{
    private Camera camera1;

    private Vector2 screensize;

    private List<Transform> allChilds = new List<Transform>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera1 = Camera.main;
        for (int i = 0; i < transform.childCount; i++)
        {
            allChilds.Add(transform.GetChild(i));
        }

        StretchToCam();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!Mathf.Approximately(screensize.x, Screen.width) || !Mathf.Approximately(screensize.y, Screen.height))
        {
            StretchToCam();
        }
    }


    private void StretchToCam()
    {
        screensize = new Vector2(Screen.width,Screen.height);
        float planeHeighScale = 2f*camera1.orthographicSize/10f;
        float planeWidthScale = planeHeighScale*camera1.aspect;
        transform.localScale = new Vector3(planeWidthScale, 1, planeHeighScale);
        
        transform.DetachChildren();
        allChilds.ForEach(x=>x.localScale=new Vector3(1,1,1));
        allChilds.ForEach(x=>x.parent=transform);
    }
}
