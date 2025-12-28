using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BorealBorder : MonoBehaviour
{
    public Material offMatt;
    public Material onMatt;
    public identityKeys thisIdentityKeys;
    private DataHolder dataHolder;
    private GridManager gridManager;

    public bool onOff = false;

    public MeshRenderer meshRenderer;

    private Vector3 spawnDir;
    

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        dataHolder = FindAnyObjectByType<DataHolder>();
        gridManager = FindAnyObjectByType<GridManager>();
        meshRenderer.material = offMatt;
        onOff = false;
        

    }

    

    public void Position()
    {
        Vector3 dir = (Vector3)dataHolder.entityIdentity[thisIdentityKeys]["Vector"];
        spawnDir = -dir;
        transform.localPosition = spawnDir*5;

    }   

    public void SpawnEntity()
    {
        GameObject spawnedEntity=Instantiate(dataHolder.intantiateDummy, transform.position+Vector3.up, Quaternion.identity);
        EntityScript spawnedEntityScript=spawnedEntity.GetComponent<EntityScript>();
      
        spawnedEntityScript.justCreated=true;

        spawnedEntityScript.gameObject.tag = "Entity";
        

        spawnedEntityScript.speed = dataHolder.speed;
        spawnedEntityScript.OnCreation(thisIdentityKeys);
        onOff = false;
        meshRenderer.material = offMatt;


    }
    private void OnMouseUpAsButton()
    {
        if (onOff)
        {
            SpawnEntity();
        }
    }

    public void SetSpawnable()
    {
        onOff = true;
        meshRenderer.material = onMatt;

    }
    
    public void SetUnSpawnable()
    {
        onOff = false;
        meshRenderer.material = offMatt;

    }
    
}
