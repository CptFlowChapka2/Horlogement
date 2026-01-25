
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public HashSet<HashSet<object>> toProcess = new HashSet<HashSet<object>>();
    public GameObject fuseFeedBack;
    private bool readyToProcess = false;

    public void AddToProcess(HashSet<object> list)
    {
        toProcess.Add(list);
        readyToProcess = true;

    }

    private void FixedUpdate()
    {
        if (readyToProcess)
        {
            if (toProcess.Count>1)
            {
               HashSet<object> lastElement=null;
               List<HashSet<object>> toRemove = new List<HashSet<object>>();
                foreach (var elements in toProcess)
                {
                    
                    if (lastElement is not null&&elements.Overlaps(lastElement))
                    {
                        toRemove.Add(elements);
                    }

                    lastElement = elements;
                }

                foreach (var remove in toRemove)
                {
                    toProcess.Remove(remove);
                }


            }
            CreateFusedEntity(toProcess);
        }
    }
    private void CreateFusedEntity(HashSet<HashSet<object>> input)
    {
        
        foreach (HashSet<object> elements in input)
        {
            HashSet<object> list = elements;
            DataHolder dataHolder= list.OfType<DataHolder>().First();
            float speed= list.OfType<float>().First();

            List<Vector3> vector3s = list.OfType<Vector3>().ToList();
            List<identityKeys> identityKeysList = list.OfType<identityKeys>().ToList();
            GameObject fusedEntity=Instantiate(dataHolder.intantiateDummy, vector3s[0], Quaternion.identity);
            EntityScript fusedEntityScript=fusedEntity.GetComponent<EntityScript>();
            fusedEntityScript.justCreated=true;

            fusedEntityScript.gameObject.tag = "Entity";

            fusedEntityScript.speed = speed;
            fusedEntityScript.OnCreation(identityKeysList[0], identityKeysList.Count < 2 ? default : identityKeysList[1],
                fusedEntityScript.CreateDir(vector3s[1], vector3s[2]));
            fusedEntityScript.soundHandler.Play(fusedEntityScript.gameObject,fusedEntityScript.thisIdentity.SoundFuse);
            SpriteRenderer sprite=Instantiate(fuseFeedBack, fusedEntity.transform.position, Quaternion.Euler(90,0,0)).GetComponent<SpriteRenderer>();
            sprite.color = fusedEntityScript.thisIdentity.ColorMain.color;


        }
        toProcess.Clear();
        readyToProcess = false;
        
    }
}