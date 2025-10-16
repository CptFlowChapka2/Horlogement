using System;
using System.Collections.Generic;
using UnityEngine;

public class Pivot_AnchorCam : MonoBehaviour
{
    public GameObject player;
    private List<GameObject> allAnchor; //une List de GameObject
    public float switchSpeed = 10f;

    private void Start()
    {
        //on Initialise la List avec tout les GameObject de la scéne (faudrait trouver un autre dénominateur commun que GameObject)
        allAnchor = new List<GameObject>(FindObjectsByType<GameObject>(FindObjectsSortMode.None));
        //on Enléve tout les GameObject qui n'ont pas le Tag CamAnchor
        allAnchor.RemoveAll(a => !a.CompareTag("CamAnchor")); // le a=> a est un Lambda 


    }

    private void Update()
    {
        //on trie la list par la distance entre 1 CamAnchor et le Joueur de maniére que le plus proche soit en premiére position
        allAnchor.Sort((a, b) => Vector3.Distance(b.transform.position, player.transform.position)
            .CompareTo(Vector3.Distance(a.transform.position, player.transform.position)));


        if (Vector3.Distance(transform.position, allAnchor[1].transform.position) > 1f)
        {
            //On Déplace la Cam vers  le premier point de la list uniquement si il sont éloigné l'un à l'autre .
            transform.position = Vector3.MoveTowards(transform.position, allAnchor[1].transform.position, switchSpeed * Time.deltaTime);

        }
    }

}