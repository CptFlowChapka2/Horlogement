using System;
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
    private BoxCollider voidCollider;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        dataHolder = FindAnyObjectByType<DataHolder>();
        gridManager = FindAnyObjectByType<GridManager>();
        meshRenderer.material = offMatt;
        onOff = false;
        voidCollider = gridManager.theVoid.GetComponent<BoxCollider>();
        Position();
    }

    

    private void Position()
    {
        Vector3 dir = (Vector3)dataHolder.entityIdentity[thisIdentityKeys]["Vector"];
        spawnDir = -dir;

        Vector3 setupDir = -dir.normalized*gridManager.testVec.magnitude;
        Ray ray = new Ray(new Vector3(gridManager.gridCenter.x,gridManager.theVoid.transform.position.y-0.75f,gridManager.gridCenter.z),-dir);
        RaycastHit[] raycastHits={};
        
        Debug.Log(Physics.RaycastNonAlloc(ray,raycastHits,Mathf.Infinity,Physics.AllLayers));
        Physics.RaycastNonAlloc(ray, raycastHits, Mathf.Infinity, Physics.AllLayers);
        //Vector3 targetPos = new Vector3(raycastHits.First().point.x,0,raycastHits.First().point.z);
        //transform.position = targetPos;


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
