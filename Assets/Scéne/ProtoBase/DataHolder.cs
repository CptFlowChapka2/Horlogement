using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public enum identityKeys
{
    notAsignated,
    color0,
    color1,
    color2
}
public class DataHolder : MonoBehaviour
{
    public GameObject intantiateDummy;

    [SerializeField] private Color color0 ;
    [SerializeField] private Color color1 ;
    [SerializeField] private Color color2 ;
    
    [SerializeField] private AudioClip sound0 ;
    [SerializeField] private AudioClip sound1 ;
    [SerializeField] private AudioClip sound2 ;

    public List<Color> allColor = new List<Color>();
    public Dictionary<identityKeys, Dictionary<string,object>> entityIdentity = new Dictionary<identityKeys, Dictionary<string,object>>();

    private void Start()
    {
        CreateColorList();
        CreateDictionary();
    }

    private void CreateDictionary()
    {
        Vector3 baseVector = new Vector3(1, 0, 0);
        
        Dictionary<string, object> identity0 = new Dictionary<string, object>
        {
            { "color", color0 },
            { "Vector", baseVector }
        };
        // identity0.Add("sound",sound0);
        entityIdentity.Add(identityKeys.color0,identity0);
        
        Dictionary<string, object> identity1 = new Dictionary<string, object>
        {
            { "color", color1 },
            //{ "Vector", Quaternion.AngleAxis(51.42f, Vector3.up) * baseVector }
            { "Vector",  -baseVector }
        };
        //identity0.Add("sound",sound1);
        entityIdentity.Add(identityKeys.color1,identity1);
        
        Dictionary<string, object> identity2 = new Dictionary<string, object>
        {
            { "color", color2 },
            { "Vector", (Quaternion.AngleAxis(102.84f, Vector3.up) * baseVector) }
        };
        //identity0.Add("sound",sound2);
        entityIdentity.Add(identityKeys.color2,identity2);
        
        

    }

    private void CreateColorList()
    {
        allColor.Add(color0);
        allColor.Add(color1);
        allColor.Add(color2);
    }
}
