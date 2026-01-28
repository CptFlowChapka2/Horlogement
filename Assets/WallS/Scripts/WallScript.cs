
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
    private Material willDestroylWall;
    private Material willDestroylWall2;
    private Material normallWall;
    private Material normallWall2;
    public Sculptor sculptor;
    private DataHolder dataHolder;
   

    private SoundHandler soundHandler;
    private bool hasCreated = false;
    
    public void Create(WallPointScript inOne,WallPointScript inTwo,GridManager inGridManager,Sculptor sculptoar,SoundHandler soundhalder,DataHolder datoHolder)
    {
        one = inOne;
        two = inTwo;
        tileSize = inGridManager.tileSize.x;
        dataHolder = datoHolder;
        sculptor = sculptoar;
        soundHandler = soundhalder;
        if (!(thisCollider && meshRenderer))
        {
            thisCollider = GetComponent<BoxCollider>();
            meshRenderer = GetComponent<MeshRenderer>(); 
        }

        willDestroylWall = dataHolder.willDestroylWall;
        normallWall = dataHolder.normallWall;
        willDestroylWall2 = dataHolder.willDestroylWall2;
        normallWall2 = dataHolder.normallWall2;

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

    private void Start()
    {
      
            thisCollider = GetComponent<BoxCollider>();
            meshRenderer = GetComponent<MeshRenderer>();
            dataHolder = FindAnyObjectByType<DataHolder>();
            willDestroylWall = dataHolder.willDestroylWall;
            normallWall = dataHolder.normallWall;
            willDestroylWall2 = dataHolder.willDestroylWall2;
            normallWall2 = dataHolder.normallWall2;
        
        
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
            Create(one,origine,gridManager,sculptor,soundHandler,dataHolder);
            
            return;
        }

        if (origine == two)
        {
           // Create(one,origine,gridManager);
            Create(newOne,two,gridManager,sculptor,soundHandler,dataHolder);
            
            return; 
        }
    }

    public void Break() 
    {
        
        if(gameObject ==null) return;
        
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

    public void SetFeedBackColor(Material color,Material color2=null)
    {

        
        meshRenderer.material = color;
        if (color2 is null) return;
        meshRenderer.materials[1] = color2;

    }

    private void OnMouseOver()
    { 
        if(sculptor.secondTilePoint)return;
        SetFeedBackColor(willDestroylWall);
        Cursor.SetCursor(dataHolder.cursorDestroyWall,Vector2.zero,CursorMode.ForceSoftware);
    }
    
    private void OnMouseExit()
    {
        Cursor.SetCursor(dataHolder.cursorNull,Vector2.zero,CursorMode.ForceSoftware);
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
