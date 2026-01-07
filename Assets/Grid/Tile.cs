using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour
{
   
   public Vector2Int coords;
   private GridManager gridManager;
   public bool isWall = false;
   public WallPointScript currentWallPointScript;

   [SerializeField]private float maxToCharge = 6;
   public float currentCharge = 0;
   [SerializeField] private float chargeSpeed = 1f;
   [SerializeField] private float deChargeSpeed = 0.5f;
   [SerializeField] private float cooldDown = 1f;

   private List<identityKeys> identityKeysList = new List<identityKeys>();

   private bool canSpawn = true;
   private DataHolder dataHolder;
   
   
   public void Initialize(Vector2Int coords, GridManager gridManager,DataHolder newDataHolder)
   {
      dataHolder=newDataHolder;
      this.coords = coords;
      this.gridManager = gridManager;
   }

   private void Update()
   {
      if (currentCharge >= maxToCharge)
      {
         currentCharge = 0;
         canSpawn = false;
         SpawnEntity();
      }
      currentCharge -= deChargeSpeed*Time.deltaTime;
      currentCharge = Mathf.Clamp(currentCharge, 0, currentCharge);
   }


   private void OnMouseDown()
   {
      Debug.Log("Clicked on " + gridManager.WorldToGridPos(transform.position));
   }

   private void OnTriggerStay(Collider other)
   {
      if (canSpawn&&currentWallPointScript==null&&(other.gameObject.CompareTag("Entity") && other.gameObject.TryGetComponent<EntityScript>(out EntityScript otherEntity)))
      {
         currentCharge += chargeSpeed*Time.deltaTime;
         identityKeysList.Add(otherEntity.thisIdentity.IdentityKey);
      }
   }

   private void SpawnEntity()
   {
      GameObject spawnedEntity=Instantiate(dataHolder.intantiateDummy, transform.position+Vector3.up, Quaternion.identity);
      EntityScript spawnedEntityScript=spawnedEntity.GetComponent<EntityScript>();
      
      spawnedEntityScript.justCreated=true;

      spawnedEntityScript.gameObject.tag = "Entity";
      
      identityKeys most = identityKeysList.GroupBy(i=>i).OrderByDescending(grp=>grp.Count())
         .Select(grp=>grp.Key).First();

      spawnedEntityScript.speed = dataHolder.speed;
      spawnedEntityScript.OnCreation(most);
      identityKeysList.Clear();
      
      
      Invoke(nameof(ReActivate),cooldDown);
   }

   private void ReActivate()
   {
      canSpawn = true;
   }
}
