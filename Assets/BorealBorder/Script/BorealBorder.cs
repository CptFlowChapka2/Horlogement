using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BorealBorder : MonoBehaviour
{
    public Material lowFeedBackMatt;
    public Material overLowFeedBackMatt;
    public Material highFeedBackMatt;
    public Material overHighFeedBackMatt;
    public Material offFeedBackMatt;
   
    public identityKeys thisIdentityKeys;
    private DataHolder dataHolder;
    private Boreal boreal;
    private GridManager gridManager;

    public bool onOff = false;

    public MeshRenderer meshRenderer;

    private Vector3 spawnDir;
    private Texture2D cursor;
    public colorState colorStated;
   
    

    private void Start()
    {
        boreal = FindAnyObjectByType<Boreal>();
        meshRenderer = GetComponent<MeshRenderer>();
        dataHolder = FindAnyObjectByType<DataHolder>();
        gridManager = FindAnyObjectByType<GridManager>();

        cursor = thisIdentityKeys switch {
            identityKeys.A => dataHolder.cursorBoreal0,
            identityKeys.B => dataHolder.cursorBoreal1,
            identityKeys.C => dataHolder.cursorBoreal2,
            identityKeys.D => dataHolder.cursorBoreal3,
            identityKeys.E => dataHolder.cursorBoreal4,
            identityKeys.F => dataHolder.cursorBoreal5,
            identityKeys.G => dataHolder.cursorBoreal6,
            _ => throw new ArgumentOutOfRangeException()
        };
        onOff = false;

        switch (thisIdentityKeys)
        {

            case identityKeys.notAsignated:
                throw new ArgumentException();
                
            case identityKeys.A:
                lowFeedBackMatt=dataHolder.borealLowFeedBackMatt0;
                overLowFeedBackMatt=dataHolder.borealOverLowFeedBackMatt0;
                highFeedBackMatt=dataHolder.borealHighFeedBackMatt0;
                overHighFeedBackMatt=dataHolder.borealOverHighFeedBackMatt0;
                offFeedBackMatt=dataHolder.borealOffFeedBackMatt0;
               
                break;
            case identityKeys.B:
                lowFeedBackMatt=dataHolder.borealLowFeedBackMatt1;
                overLowFeedBackMatt=dataHolder.borealOverLowFeedBackMatt1;
                highFeedBackMatt=dataHolder.borealHighFeedBackMatt1;
                overHighFeedBackMatt=dataHolder.borealOverHighFeedBackMatt1;
                offFeedBackMatt=dataHolder.borealOffFeedBackMatt1;
               
                break;
            case identityKeys.C:
                lowFeedBackMatt=dataHolder.borealLowFeedBackMatt2;
                overLowFeedBackMatt=dataHolder.borealOverLowFeedBackMatt2;
                highFeedBackMatt=dataHolder.borealHighFeedBackMatt2;
                overHighFeedBackMatt=dataHolder.borealOverHighFeedBackMatt2;
                offFeedBackMatt=dataHolder.borealOffFeedBackMatt2;
               
                break;
            case identityKeys.D:
                lowFeedBackMatt=dataHolder.borealLowFeedBackMatt3;
                overLowFeedBackMatt=dataHolder.borealOverLowFeedBackMatt3;
                highFeedBackMatt=dataHolder.borealHighFeedBackMatt3;
                overHighFeedBackMatt=dataHolder.borealOverHighFeedBackMatt3;
                offFeedBackMatt=dataHolder.borealOffFeedBackMatt3;
               
                break;
            case identityKeys.E:
                lowFeedBackMatt=dataHolder.borealLowFeedBackMatt4;
                overLowFeedBackMatt=dataHolder.borealOverLowFeedBackMatt4;
                highFeedBackMatt=dataHolder.borealHighFeedBackMatt4;
                overHighFeedBackMatt=dataHolder.borealOverHighFeedBackMatt4;
                offFeedBackMatt=dataHolder.borealOffFeedBackMatt4;
               
                break;
            case identityKeys.F:
                lowFeedBackMatt=dataHolder.borealLowFeedBackMatt5;
                overLowFeedBackMatt=dataHolder.borealOverLowFeedBackMatt5;
                highFeedBackMatt=dataHolder.borealHighFeedBackMatt5;
                overHighFeedBackMatt=dataHolder.borealOverHighFeedBackMatt5;
                offFeedBackMatt=dataHolder.borealOffFeedBackMatt5;
               
                break;
            case identityKeys.G:
                lowFeedBackMatt=dataHolder.borealLowFeedBackMatt6;
                overLowFeedBackMatt=dataHolder.borealOverLowFeedBackMatt6;
                highFeedBackMatt=dataHolder.borealHighFeedBackMatt6;
                overHighFeedBackMatt=dataHolder.borealOverHighFeedBackMatt6;
                offFeedBackMatt=dataHolder.borealOffFeedBackMatt6;
               
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        meshRenderer.material = offFeedBackMatt;
        
        
        
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

    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursor,Vector2.zero,CursorMode.ForceSoftware);
        
        
    }
    private void OnMouseExit()
    {
        Cursor.SetCursor(dataHolder.cursorNull,Vector2.zero,CursorMode.ForceSoftware);
        
        
    }

    public void SetSpawnable()
    {
        onOff = true;

    }

    public void ChangeMat(int mayhaps)
    {
       

        meshRenderer.material = mayhaps switch
        {
            -1 => offFeedBackMatt,
            0 => lowFeedBackMatt,
            1 => highFeedBackMatt,
            _ => meshRenderer.material
        };

        colorStated= mayhaps switch
        {
            -1 => colorState.off,
            0 => colorState.low,
            1 => colorState.high,
            _ => colorStated
        };




    }
    
    public void SetUnSpawnable()
    {
        onOff = false;
        
    }

    private void OverChangeMat(bool maybe)
    {
        
        if (maybe)
        {
            meshRenderer.material = colorStated switch
            {
                colorState.off => offFeedBackMatt,
                colorState.low => overLowFeedBackMatt,
                colorState.high => overHighFeedBackMatt,
                _ => meshRenderer.material
            };
        }
        else
        {
            meshRenderer.material = colorStated switch
            {
                colorState.off => offFeedBackMatt,
                colorState.low => lowFeedBackMatt,
                colorState.high => highFeedBackMatt,
                _ => meshRenderer.material
            };
        }
        
          
        
        


    }

}

 public enum colorState
{
    high,
    
    low,
   
    off
    
}