using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boreal : MonoBehaviour
{
    public List<EntityScript> allEntity = new List<EntityScript>();
    public float cooldownMax = 0.6f;
    public float cooldownCurrent = 0f;
    private List<BorealBorder> allBorder = new List<BorealBorder>();
    private List<BorealBorder> allActiveBorder = new List<BorealBorder>();
    private List<GameObject> allVoidWall = new List<GameObject>();
    private GameObject theVoid;
    
    public float checkFrequency = 2.5f;
    public int minEntity = 2;

    private HashSet<identityKeys> keyInPlay=new HashSet<identityKeys>();

    private DataHolder dataHolder;

    private void Start()
    {
        FindObjectsByType<BorealBorder>(FindObjectsInactive.Exclude,FindObjectsSortMode.None).ToList().ForEach(x=>allBorder.Add(x));
        allVoidWall = GameObject.FindGameObjectsWithTag("VoidWall").ToList();
        theVoid = GameObject.FindGameObjectsWithTag("Void").First();
        dataHolder = FindAnyObjectByType<DataHolder>();
        
        
        
        
        RandomStartingSet(dataHolder.startingNbr);
        
        foreach (BorealBorder borealBorder in allBorder.FindAll(x => keyInPlay.Contains(x.thisIdentityKeys)))
        {
            allActiveBorder.Add(borealBorder);
           
        }
        foreach (BorealBorder borealBorder in allBorder)
        {
            borealBorder.ChangeMat(-1);
            borealBorder.SetUnSpawnable();
        }

        foreach (var border in allActiveBorder)
        {
            border.ChangeMat(0);
            border.SetSpawnable();
        }
        
        
    }
    private void Update()
    {
        Check();
    }

    private void Check()
    { 
        allEntity.Clear();
        FindObjectsByType<EntityScript>(FindObjectsInactive.Exclude,FindObjectsSortMode.None).ToList().ForEach(x=>allEntity.Add(x));

        foreach (EntityScript entity in allEntity.Where(entity => !keyInPlay.Contains(entity.thisIdentity.IdentityKey)))
        {
            keyInPlay.Add(entity.thisIdentity.IdentityKey);

            BorealBorder newBB = allBorder.Find(x => x.thisIdentityKeys.Equals(entity.thisIdentity.IdentityKey));
            newBB.ChangeMat(0);
            newBB.SetSpawnable();
            allActiveBorder.Add(newBB);

        }
        

        if ((allEntity.Count >= minEntity))
        {
            allActiveBorder.ForEach(x=>x.ChangeMat(0));
        }
        else
        {
            allActiveBorder.ForEach(x=>x.ChangeMat(1));
        }

        if (cooldownCurrent<=cooldownMax)
        {
            allActiveBorder.ForEach(x=>x.SetUnSpawnable());
            cooldownCurrent += 1 * Time.deltaTime;
            return;
        }
        allActiveBorder.ForEach(x=>x.SetSpawnable());
        
        
    }

    public void MooveBorder()
    {
        
        allBorder.ForEach(x=>x.Position());
       
    }


    private void RandomStartingSet(int nbr)
    {
        while (keyInPlay.Count<nbr)
        {
            keyInPlay.Add((identityKeys)Random.Range(1, 6));
        }
    }
}
