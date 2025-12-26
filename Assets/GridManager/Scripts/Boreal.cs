using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boreal : MonoBehaviour
{
    public List<GameObject> allEntity = new List<GameObject>();
    private List<BorealBorder> allBorder = new List<BorealBorder>();
    
    public float checkFrequency = 2.5f;
    public int minEntity = 2;


    private void Start()
    {
        FindObjectsByType<BorealBorder>(FindObjectsInactive.Exclude,FindObjectsSortMode.None).ToList().ForEach(x=>allBorder.Add(x));
        InvokeRepeating(nameof(Check),checkFrequency,checkFrequency);
    }

    private void Check()
    { 
        FindObjectsByType<EntityScript>(FindObjectsInactive.Exclude,FindObjectsSortMode.None).ToList().ForEach(x=>allEntity.Add(x.gameObject));
        
        if(!(allEntity.Count<minEntity)) return;
        allBorder.ForEach(x=>x.SetSpawnable());
        
        
    }
}
