using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sculptor : MonoBehaviour
{
    private WallCreator wallCreator;


    [SerializeField] private float wallCreationCounterMax = 2f;
    [SerializeField] private float wallCreationCounter;
    [SerializeField] private float mouseSpeed = 2f;
    private InputAction mouse;
    private InputAction clic;

    public List<GameObject> toDestroy = new List<GameObject>();

    private Vector3 mooveTry;

    public WallPointScript currentSelection;

    private void Start()
    {
        wallCreator = FindAnyObjectByType<WallCreator>();

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

        if (!Physics.Raycast(ray, out RaycastHit hit) || !hit.collider.CompareTag("WallPoint")) return;
        WallPointScript hitWallPoint = hit.collider.gameObject.GetComponent<WallPointScript>();

        currentSelection = hitWallPoint;
        currentSelection.isSelected = true;
        mooveTry = currentSelection.transform.position;

    }

    private void MooveSelected()
    {
       
        Vector2 mouseMoove = mouse.ReadValue<Vector2>();
        Vector3 resultMouvment = new Vector3(mouseMoove.x, 0, mouseMoove.y) * (Time.deltaTime * mouseSpeed);
        if (Vector3.Distance(mooveTry + resultMouvment, currentSelection.linkedTile.transform.position) <= currentSelection.maxMouvment)
        {
            mooveTry += resultMouvment;
        }

        if (Vector3.Distance(mooveTry, currentSelection.linkedTile.transform.position) <= currentSelection.maxMouvment)
        {
            wallCreationCounter = 0;
            mooveTry += resultMouvment;
            currentSelection.transform.position += resultMouvment;
            currentSelection.walls.ForEach(x => x.Moove());
        }
        else if (wallCreationCounter >= wallCreationCounterMax)
        {

            wallCreationCounter = 0;
            Vector3 cachePos = currentSelection.transform.position;
            WallPointScript cache = currentSelection;
            currentSelection.transform.position = currentSelection.linkedTile.transform.position;
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

            wallCreationCounter += Time.deltaTime;

        }
    }

    

    private void FinishMouvement()
    {
        
        if (clic.IsPressed()) return;

        currentSelection.isSelected = false;
        currentSelection.EndMouvement();

        currentSelection = null;

    }

    

}