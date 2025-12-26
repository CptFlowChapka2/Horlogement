using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public enum identityKeys
{
    notAsignated,
    color0,
    color1,
    color2,
    color3,
    color4,
    color5,
    color6,
}
public class DataHolder : MonoBehaviour
{
    public GameObject intantiateDummy;

    [SerializeField] private Color color0 ;
    [SerializeField] private Color color1 ;
    [SerializeField] private Color color2 ;
    [SerializeField] private Color color3 ;
    [SerializeField] private Color color4 ;
    [SerializeField] private Color color5;
    [SerializeField] private Color color6;
    
    [SerializeField] private AudioClip sound0 ;
    [SerializeField] private AudioClip sound1 ;
    [SerializeField] private AudioClip sound2 ;
    [SerializeField] private AudioClip sound3 ;
    [SerializeField] private AudioClip sound4 ;
    [SerializeField] private AudioClip sound5 ;
    [SerializeField] private AudioClip sound6 ;
    
    public float speed =1;
    public float addedAngle =51.42f;

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
            { "Vector", baseVector },
            {"sound",sound0}
        };
        
        entityIdentity.Add(identityKeys.color0,identity0);
        
        Dictionary<string, object> identity1 = new Dictionary<string, object>
        {
            { "color", color1 },
            { "Vector", Quaternion.AngleAxis(addedAngle, Vector3.up) * baseVector },
            {"sound",sound1}
        };
        
        entityIdentity.Add(identityKeys.color1,identity1);
        
        Dictionary<string, object> identity2 = new Dictionary<string, object>
        {
            { "color", color2 },
            { "Vector", (Quaternion.AngleAxis(addedAngle*2, Vector3.up) * baseVector) },
            {"sound",sound2}
        };
        
        entityIdentity.Add(identityKeys.color2,identity2);
        Dictionary<string, object> identity3 = new Dictionary<string, object>
        {
            { "color", color3 },
            { "Vector", (Quaternion.AngleAxis(addedAngle*3, Vector3.up) * baseVector) },
            {"sound",sound3}
        };
        
        entityIdentity.Add(identityKeys.color3,identity3);
        Dictionary<string, object> identity4 = new Dictionary<string, object>
        {
            { "color", color4 },
            { "Vector", (Quaternion.AngleAxis(addedAngle*4, Vector3.up) * baseVector) },
            {"sound",sound4}
        };
        
        entityIdentity.Add(identityKeys.color4,identity4);
        Dictionary<string, object> identity5 = new Dictionary<string, object>
        {
            { "color", color5 },
            { "Vector", (Quaternion.AngleAxis(addedAngle*5, Vector3.up) * baseVector) },
            {"sound",sound5}
        };
        
        entityIdentity.Add(identityKeys.color5,identity5);
        
        Dictionary<string, object> identity6 = new Dictionary<string, object>
        {
            { "color", color6 },
            { "Vector", (Quaternion.AngleAxis(addedAngle*6, Vector3.up) * baseVector) },
            {"sound",sound6}
        };
        
        entityIdentity.Add(identityKeys.color6,identity6);

    }

    private void CreateColorList()
    {
        allColor.Add(color0);
        allColor.Add(color1);
        allColor.Add(color2);
        allColor.Add(color3);
        allColor.Add(color4);
        allColor.Add(color5);
        allColor.Add(color6);
    }
}
