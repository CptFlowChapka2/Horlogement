using System;
using Unity.VisualScripting;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    public WallPointScript one;
    public WallPointScript two;
    public float length;
    private float tileSize;

    private void Update()
    {
        
    }
    public void Create(WallPointScript inOne,WallPointScript inTwo,GridManager inGridManager)
    {
        one = inOne;
        two = inTwo;
        tileSize = inGridManager.tileSize.x;
        
        one.walls.Add(this);
        two.walls.Add(this);
        
        Moove();
        
    }

    public void Moove()
    {
        
        
        Vector3 a = one.transform.position; 
        Vector3 b = two.transform.position;
        
        
        Vector3 mid = (a + b) / 2f;

        transform.position = mid;
        
        Vector3 diff = b - a;
        diff.y = 0;
        
        length = diff.magnitude;
        
        transform.localScale = new Vector3(0.3f, 5f, length);
        transform.rotation = Quaternion.LookRotation(diff);
    }

    public void Break()
    {
        one.walls.Remove(this);
        two.walls.Remove(this);
        DestroyImmediate(gameObject);
    }
    
}
