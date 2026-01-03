using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boreal : MonoBehaviour
{
    public List<GameObject> allEntity = new List<GameObject>();
    private List<BorealBorder> allBorder = new List<BorealBorder>();
    private List<GameObject> allVoidWall = new List<GameObject>();
    private GameObject theVoid;
    
    public float checkFrequency = 2.5f;
    public int minEntity = 2;


    private void Start()
    {
        FindObjectsByType<BorealBorder>(FindObjectsInactive.Exclude,FindObjectsSortMode.None).ToList().ForEach(x=>allBorder.Add(x));
        allVoidWall = GameObject.FindGameObjectsWithTag("VoidWall").ToList();
        theVoid = GameObject.FindGameObjectsWithTag("Void").First();
        
    }
    private void Update()
    {
        Check();
    }

    private void Check()
    { 
        FindObjectsByType<EntityScript>(FindObjectsInactive.Exclude,FindObjectsSortMode.None).ToList().ForEach(x=>allEntity.Add(x.gameObject));

        if (!(allEntity.Count < minEntity))
        {
            allBorder.ForEach(x=>x.SetUnSpawnable());
            return;
        }
        allBorder.ForEach(x=>x.SetSpawnable());
        
        
    }

    public void MooveBorder()
    {
        
        allBorder.ForEach(x=>x.Position());
       
    }
}
