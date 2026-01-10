using System;
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

    
    public void Create(WallPointScript inOne,WallPointScript inTwo,GridManager inGridManager)
    {
        one = inOne;
        two = inTwo;
        tileSize = inGridManager.tileSize.x;
        thisCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
        
        one.walls.Add(this);
        two.walls.Add(this);
        
        
        Moove();
        
    }

    public void Moove(bool check=true)
    {
        
        
        Vector3 a = one.transform.position; 
        Vector3 b = two.transform.position;
        
        
        Vector3 mid = (a + b) / 2f;

        transform.position = mid;
        
        Vector3 diff = b - a;
        diff.y = 0;
        
        length = diff.magnitude;
        if (!check&&length == 0)
        {
            Debug.Log("lenth is 0");
            Break();
            return;
        }
        
        transform.localScale = new Vector3(0.3f, 5f, length);
        transform.rotation = Quaternion.LookRotation(diff);
    }

    public void MergeWalls(WallPointScript newOne,WallPointScript origine,GridManager gridManager)
    {
        if (one == newOne || two == newOne )
        {
            Break();
            return;
        }

        if (origine == one)
        {
            Create(newOne,two,gridManager);
            return;
        }

        if (origine == two)
        {
            Create(one,origine,gridManager);
            return;
        }
    }

    public void Break()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        one.walls.Remove(this);
        two.walls.Remove(this);
    }

    public void ToggleColision(bool maybe)
    {
        thisCollider.isTrigger = maybe;
    }

    public void SetFeedBackColor(Color color)
    {
        meshRenderer.material.color = color;
    }

}
