using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private SortedSet<SortedSet<object>> toProcess = new SortedSet<SortedSet<object>>();


    // private void CreateFusedEntity(SortedSet<object> list)
    // {
    //     DataHolder dataHolder = (DataHolder)list.Find(x => x is DataHolder);
    //     Vector3 positionToInstantiate =
    //         GameObject fusedEntity = Instantiate(dataHolder.intantiateDummy, transform.position, Quaternion.identity);
    //     EntityScript fusedEntityScript = fusedEntity.GetComponent<EntityScript>();
    //     fusedEntityScript.justCreated = true;
    //
    //     fusedEntityScript.isDummy = false;
    //     fusedEntityScript.speed = speed;
    // }
}
