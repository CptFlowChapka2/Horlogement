using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class WallPointScript : MonoBehaviour
{

    private InputAction mouse;
    public Tile linkedTile;
    [SerializeField] private float mouseSpeed = 2f;
    [SerializeField] private float anchorCheckRange = 2f;
     private float maxMouvment = 2f;
    private SphereCollider thisCollider;
    public GridManager gridManager;
    


    private void Start()
    {
        mouse = InputSystem.actions.FindAction("Look");
        thisCollider = GetComponent<SphereCollider>();
    }
    public void Create(Tile tile,GridManager gridManagerIn)
    {
        gridManager = gridManagerIn;
        maxMouvment = gridManager.tileSize.magnitude;
        linkedTile = tile;
        tile.currentWallPointScript = this;
        gameObject.transform.position = linkedTile.transform.position;
    }

    
    private void OnMouseDrag()
    {
        
        Vector2 mouseMoove = mouse.ReadValue<Vector2>();
        Vector3 resultMouvment = new Vector3(mouseMoove.x, 0, mouseMoove.y) * Time.deltaTime * mouseSpeed;
        if ((transform.position - (transform.position + resultMouvment)).magnitude <= maxMouvment)
        { 
            transform.position += resultMouvment;
        }
       
    }

    private void OnMouseUp()
    {
        
        Collider[] results = new Collider[] { };
        Physics.OverlapSphereNonAlloc(transform.position, anchorCheckRange, results, 0, QueryTriggerInteraction.Collide);

       

        if (results.Length==0)
        {
            transform.position = linkedTile.transform.position;
            return;
        }

       results= results.OrderBy((d) => (d.transform.position - transform.position).sqrMagnitude).ToArray();

       if (results.First().Equals(thisCollider)){ transform.position = linkedTile.transform.position;return;}
       if (!results.First().gameObject.CompareTag("Anchor") || !results.First().TryGetComponent<Tile>(out Tile foundTile)) return;
       
       linkedTile = foundTile;
       transform.position = foundTile.transform.position;

    }
}
