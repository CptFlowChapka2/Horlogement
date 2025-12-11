using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public HashSet<List<object>> toProcess = new HashSet<List<object>>();
    private bool readyToProcess = false;

    public void AddToProcess(List<object> list)
    {
        toProcess.Add(list);
        readyToProcess = true;

    }

    private void FixedUpdate()
    {
        if (readyToProcess)
        {
            CreateFusedEntity(toProcess);
        }
    }
    private void CreateFusedEntity(HashSet<List<object>> input)
    {
        
        foreach (List<object> elements in input)
        {
            List<object> list = elements;
            DataHolder dataHolder = (DataHolder)list.Find(x=>x is DataHolder);
            List<Vector3> vector3s =(List<Vector3>)list.Find(x=>x is List<Vector3>) ;
            float speed =(float)list.Find(x=>x is float) ;
            List<identityKeys> identityList =(List<identityKeys>)list.Find(x=>x is List<identityKeys>) ;
        
            GameObject fusedEntity=Instantiate(dataHolder.intantiateDummy, vector3s[0], Quaternion.identity);
            EntityScript fusedEntityScript=fusedEntity.GetComponent<EntityScript>();
            fusedEntityScript.justCreated=true;

            fusedEntityScript.isDummy = false;
            fusedEntityScript.speed =speed ;
        
            fusedEntityScript.OnCreation( identityList[0],identityList[1], fusedEntityScript.CreateDir(vector3s[1], vector3s[2]));

            
        }
        toProcess.Clear();
        readyToProcess = false;
        
    }
}
