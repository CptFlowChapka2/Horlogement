using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BorealBorder : MonoBehaviour
{
    public Material lowFeedBackMatt;
    public Material highFeedBackMatt;
    public identityKeys thisIdentityKeys;
    private DataHolder dataHolder;
    private Boreal boreal;
    private GridManager gridManager;

    public bool onOff = false;

    public MeshRenderer meshRenderer;

    private Vector3 spawnDir;
    

    private void Start()
    {
        boreal = FindAnyObjectByType<Boreal>();
        meshRenderer = GetComponent<MeshRenderer>();
        dataHolder = FindAnyObjectByType<DataHolder>();
        gridManager = FindAnyObjectByType<GridManager>();
        meshRenderer.material = lowFeedBackMatt;
        onOff = false;
        

    }

    

    public void Position()
    {
        Vector3 dir = (Vector3)dataHolder.entityIdentity[thisIdentityKeys]["Vector"];
        spawnDir = -dir;
        transform.localPosition = spawnDir.normalized*4.8f;

        Transform parent = transform.parent;
        transform.parent = null;
        transform.localScale=Vector3.one;//ici la taille du truc
        transform.parent = parent;

    }   

    public void SpawnEntity()
    {
        boreal.cooldownCurrent = 0;
        GameObject spawnedEntity=Instantiate(dataHolder.intantiateDummy, transform.position+Vector3.up, Quaternion.identity);
        EntityScript spawnedEntityScript=spawnedEntity.GetComponent<EntityScript>();
      
        spawnedEntityScript.justCreated=true;

        spawnedEntityScript.gameObject.tag = "Entity";
        

        spawnedEntityScript.speed = dataHolder.speed;
        spawnedEntityScript.OnCreation(thisIdentityKeys);
        onOff = false;
        meshRenderer.material = lowFeedBackMatt;


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

    }

    public void ChangeMat(bool mayhaps)
    {
        if (mayhaps)
        {
            meshRenderer.material = highFeedBackMatt;
            return;
        }
        meshRenderer.material = lowFeedBackMatt;
    }
    
    public void SetUnSpawnable()
    {
        onOff = false;
        
    }
    
}
