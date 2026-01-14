using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    public Collider thisCollider;
    public WallPointScript one;
    public WallPointScript two;
    public float length;
    private float tileSize;
    private MeshRenderer meshRenderer;
    public bool coroutineRunning = false;
    public Material willDestroylWall;
    public Material normallWall;
    private Sculptor sculptor;
    public Material illegalWall;
    public Material phantomlWall;

    private SoundHandler soundHandler;
    private bool hasCreated = false;
    
    public void Create(WallPointScript inOne,WallPointScript inTwo,GridManager inGridManager,Sculptor sculptoar,SoundHandler soundhalder)
    {
        one = inOne;
        two = inTwo;
        tileSize = inGridManager.tileSize.x;
        sculptor = sculptoar;
        soundHandler = soundhalder;
        if (!(thisCollider && meshRenderer))
        {
            thisCollider = GetComponent<BoxCollider>();
            meshRenderer = GetComponent<MeshRenderer>(); 
        }

        if (one == two)
        {
            Break();
            return;
        }
        one.walls.Add(this);
        two.walls.Add(this);


        if (!soundHandler.CheckByKey(gameObject))
        {soundhalder.CreateAudioSource(gameObject);
        }
        hasCreated = true;
        Moove();
        
    }

    public void Moove(bool check=true)
    {
        if (one == two)
        {
           
            
            Break();
            
            return;
        }
        
        Vector3 a = one.transform.position; 
        Vector3 b = two.transform.position;
        
        
        Vector3 mid = (a + b) / 2f;

        transform.position = mid;
        
        Vector3 diff = b - a;
        diff.y = 0;
        
        length = diff.magnitude;
        if (!check&&length == 0)
        {
            Break();
            return;
        }

        if (hasCreated)
        {
            soundHandler.Moove(gameObject);
        }
       
        transform.localScale = new Vector3(0.2f, 5f, length);
        transform.rotation = Quaternion.LookRotation(diff);
    }

    public void MergeWalls(WallPointScript newOne,WallPointScript origine,GridManager gridManager)
    {
        if (one == newOne || two == newOne )
        {
            Debug.Log("oi");
            Break();
            return;
        }

        if (origine == one)
        {
            //Create(newOne,two,gridManager);
            Create(one,origine,gridManager,sculptor,soundHandler);
            
            return;
        }

        if (origine == two)
        {
           // Create(one,origine,gridManager);
            Create(newOne,two,gridManager,sculptor,soundHandler);
            
            return; 
        }
    }

    public void Break() 
    {
        if(gameObject is null) return;
        
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (one != null)
        { one.walls.Remove(this);
            one.CheckWalls();
            two.CheckWalls();
        }
        
        if (two != null)
        { two.walls.Remove(this);
            one.CheckWalls();
            two.CheckWalls();
        }
    }

    public void ToggleColision(bool maybe)
    {
        thisCollider.isTrigger = maybe;
    }

    public void SetFeedBackColor(Material color)
    {
        
        meshRenderer.material = color;
        
    }

    private void OnMouseOver()
    {
        if(sculptor.secondTilePoint)return;
        SetFeedBackColor(willDestroylWall);
    }
    private void OnMouseExit()
    {
        if (sculptor.secondTilePoint)
        {
            SetFeedBackColor(normallWall);
        }
        if (!sculptor.secondTilePoint || meshRenderer.material == willDestroylWall)
        {
            SetFeedBackColor(normallWall);
        }
        
    }

    


}
