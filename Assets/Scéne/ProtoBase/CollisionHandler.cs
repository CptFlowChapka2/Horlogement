using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public HashSet<HashSet<object>> toProcess = new HashSet<HashSet<object>>();
    private bool readyToProcess = false;

    public void AddToProcess(HashSet<object> list)
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
    private void CreateFusedEntity(HashSet<HashSet<object>> input)
    {
        
        foreach (HashSet<object> elements in input)
        {
            HashSet<object> list = elements;
            DataHolder dataHolder= list.OfType<DataHolder>().First();
            HashSet<Vector3> vector3Hash= list.OfType<HashSet<Vector3>>().First();
            HashSet<identityKeys> identityHash= list.OfType<HashSet<identityKeys>>().First();
            float speed= list.OfType<float>().First();

            List<Vector3> vector3s = vector3Hash.ToList();
            List<identityKeys> identityKeysList = identityHash.ToList();
            GameObject fusedEntity=Instantiate(dataHolder.intantiateDummy, vector3s[0], Quaternion.identity);
            EntityScript fusedEntityScript=fusedEntity.GetComponent<EntityScript>();
            fusedEntityScript.justCreated=true;

            fusedEntityScript.gameObject.tag = "Entity";
            
            fusedEntityScript.speed =speed ;
        
            fusedEntityScript.OnCreation( identityKeysList[0],identityKeysList[1], fusedEntityScript.CreateDir(vector3s[1], vector3s[2]));

            
        }
        toProcess.Clear();
        readyToProcess = false;
        
    }
}
