using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sculptor : MonoBehaviour
{
    private WallCreator wallCreator;


    [SerializeField] private float wallCreationCounterMax = 2f;
    [SerializeField] private float wallCreationCounter;
    [SerializeField] private float mouseSpeed = 2f;
    [SerializeField] private float checkSize = 1.5f;
    [SerializeField] private bool unCollideWallOnMouv = true;
    private InputAction mouse;
    private InputAction clic;

    

    private Vector3 mooveTry;
    private SphereCollider mooveTryOrigine;

    public WallPointScript currentSelection;

    private void Start()
    {
        wallCreator = FindAnyObjectByType<WallCreator>();
        mooveTryOrigine = GameObject.FindGameObjectWithTag("MooveTry").GetComponent<SphereCollider>();
        mouse = InputSystem.actions.FindAction("Look");
        clic = InputSystem.actions.FindAction("Clic");
    }
    private void Update()
    {
        SelectWallPointScript();
        if (currentSelection is null) return;
        MooveSelected();
        FinishMouvement();

    }


    private void SelectWallPointScript()
    {


        if (!clic.WasPressedThisFrame()) return;
        if (currentSelection is not null) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits =Physics.RaycastAll(ray);
        Debug.DrawRay(ray.origin,ray.direction*300f,Color.magenta,200f);
        
        if (hits.Length<1) return;
        List<RaycastHit> hitsList = hits.ToList();

        hitsList.RemoveAll(x => !x.collider.gameObject.CompareTag("WallPoint"));
        if (hitsList.Count<1) return;
        hitsList = hitsList.OrderBy(
            x => Vector3.Distance(new Vector3(ray.origin.x,x.transform.position.y,ray.origin.z),x.transform.position)
        ).ToList();

        if( !hitsList.First().collider.gameObject.TryGetComponent(out WallPointScript hitWallPoint ))return;
        currentSelection = hitWallPoint;
        currentSelection.isSelected = true;
        mooveTry = currentSelection.transform.position;
        mooveTryOrigine.transform.position = currentSelection.linkedTile.transform.position;
        mooveTryOrigine.radius = currentSelection.maxMouvment/2;

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(mooveTry,Vector3.one);
    }
    private void MooveSelected()
    {
       
        Vector2 mouseMoove = mouse.ReadValue<Vector2>();
        Vector3 resultMouvment = new Vector3(mouseMoove.x, 0, mouseMoove.y) * (Time.deltaTime * mouseSpeed);
        resultMouvment = Vector3.ClampMagnitude(resultMouvment,10);
        if ((Vector3.Distance(mooveTry + resultMouvment, currentSelection.linkedTile.transform.position) <=
              currentSelection.maxMouvment))
        {
            mooveTry += resultMouvment;
            
        }
        
        mooveTry += resultMouvment;
        if (Vector3.Distance(mooveTry, currentSelection.linkedTile.transform.position) <= currentSelection.maxMouvment)
        {
            wallCreationCounter = 0;
            mooveTry += resultMouvment;
            currentSelection.transform.position = mooveTryOrigine.ClosestPoint(mooveTry);
            
            if (unCollideWallOnMouv)
            {
                currentSelection.walls.ForEach(x=>x.ToggleColision(true));
            }
            currentSelection.walls.ForEach(x => x.Moove());
        }
        else if (wallCreationCounter >= wallCreationCounterMax)
        {

            wallCreationCounter = 0;
            Vector3 cachePos = currentSelection.transform.position;
            WallPointScript cache = currentSelection;
            currentSelection.transform.position = currentSelection.linkedTile.transform.position;
            if (unCollideWallOnMouv)
            {
                currentSelection.walls.ForEach(x=>x.ToggleColision(false));
            }
            currentSelection.walls.ForEach(x => x.Moove());
            ;
            currentSelection.isSelected = false;

            currentSelection = wallCreator.ExtendWallPoint(cachePos);
            currentSelection.isSelected = true;
            currentSelection.linkedTile = cache.linkedTile;
            wallCreator.ExtendWall(currentSelection, cache);


        }
        else
        {
            currentSelection.transform.position = mooveTryOrigine.ClosestPoint(mooveTry);
            if (unCollideWallOnMouv)
            {
                currentSelection.walls.ForEach(x=>x.ToggleColision(true));
            }
            currentSelection.walls.ForEach(x => x.Moove());

            wallCreationCounter += Time.deltaTime;
        }

    }

    

    private void FinishMouvement()
    {
        
        if (clic.IsPressed()) return;

        currentSelection.isSelected = false;
        currentSelection.EndMouvement();
        currentSelection.walls.ForEach(x=>x.ToggleColision(false));

        currentSelection = null;

    }

    
    

}