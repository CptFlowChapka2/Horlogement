using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

    [SerializeField] private Material color0 ;
    [SerializeField] private Material color1 ;
    [SerializeField] private Material color2 ;
    [SerializeField] private Material color3 ;
    [SerializeField] private Material color4 ;
    [SerializeField] private Material color5;
    [SerializeField] private Material color6;
    
    
    [Space][Header("identity 0")][Space]
    [SerializeField] private AudioClip sound_0_bounce ;
    [SerializeField] private AudioClip sound_0_fuse ;
    
    [Space][Header("identity 1")][Space]
    
    [SerializeField] private AudioClip sound_1_bounce ;
    [SerializeField] private AudioClip sound_1_fuse ;
   
    [Space][Header("identity 2")][Space]
    
    [SerializeField] private AudioClip sound_2_bounce ;
    [SerializeField] private AudioClip sound_2_fuse ;
    [Space][Header("identity 3")][Space]

    [SerializeField] private AudioClip sound_3_bounce ;
    [SerializeField] private AudioClip sound_3_fuse ;
    
    
    [Space][Header("identity 4")][Space]

    [SerializeField] private AudioClip sound_4_bounce ;
    [SerializeField] private AudioClip sound_4_fuse ;
    
    
    [Space][Header("identity 5")][Space]

    [SerializeField] private AudioClip sound_5_bounce ;
    [SerializeField] private AudioClip sound_5_fuse ;
    
    
    [Space][Header("identity 6")][Space]

    [SerializeField] private AudioClip sound_6_bounce ;
    [SerializeField] private AudioClip sound_6_fuse ;

    
    
    public float speed =1;
    public float addedAngle =51.42f;

    public List<Material> allColor = new List<Material>();
    
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
            {"sound",CreateSoundDico(identityKeys.color0)}

        };
        
        entityIdentity.Add(identityKeys.color0,identity0);
        
        Dictionary<string, object> identity1 = new Dictionary<string, object>
        {
            { "color", color1 },
            { "Vector", Quaternion.AngleAxis(addedAngle, Vector3.up) * baseVector },
            {"sound",CreateSoundDico(identityKeys.color1)}
            
        };
        
        entityIdentity.Add(identityKeys.color1,identity1);
        
        Dictionary<string, object> identity2 = new Dictionary<string, object>
        {
            { "color", color2 },
            { "Vector", (Quaternion.AngleAxis(addedAngle*2, Vector3.up) * baseVector) },
            {"sound",CreateSoundDico(identityKeys.color2)}

        };
        
        entityIdentity.Add(identityKeys.color2,identity2);
        Dictionary<string, object> identity3 = new Dictionary<string, object>
        {
            { "color", color3 },
            { "Vector", (Quaternion.AngleAxis(addedAngle*3, Vector3.up) * baseVector) },
            {"sound",CreateSoundDico(identityKeys.color3)}

        };
        
        entityIdentity.Add(identityKeys.color3,identity3);
        Dictionary<string, object> identity4 = new Dictionary<string, object>
        {
            { "color", color4 },
            { "Vector", (Quaternion.AngleAxis(addedAngle*4, Vector3.up) * baseVector) },
            {"sound",CreateSoundDico(identityKeys.color4)}

        };
        
        entityIdentity.Add(identityKeys.color4,identity4);
        Dictionary<string, object> identity5 = new Dictionary<string, object>
        {
            { "color", color5 },
            { "Vector", (Quaternion.AngleAxis(addedAngle*5, Vector3.up) * baseVector) },
            {"sound",CreateSoundDico(identityKeys.color5)}

        };
        
        entityIdentity.Add(identityKeys.color5,identity5);
        
        Dictionary<string, object> identity6 = new Dictionary<string, object>
        {
            { "color", color6 },
            { "Vector", (Quaternion.AngleAxis(addedAngle*6, Vector3.up) * baseVector) },
            {"sound",CreateSoundDico(identityKeys.color6)}
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

    private Dictionary<string,AudioClip> CreateSoundDico(identityKeys identityKeys)
    {
        Dictionary<string, AudioClip> toReturn = new Dictionary<string, AudioClip>();
        switch (identityKeys)
        {

            case identityKeys.notAsignated:
                throw new InvalidDataException();
                break;
            case identityKeys.color0:
                toReturn.Add("bounce",sound_0_bounce);
                toReturn.Add("fuse",sound_0_fuse);
                break;
            case identityKeys.color1:
                toReturn.Add("bounce",sound_1_bounce);
                toReturn.Add("fuse",sound_1_fuse);
                break;
            case identityKeys.color2:
                toReturn.Add("bounce",sound_2_bounce);
                toReturn.Add("fuse",sound_2_fuse);
                break;
            case identityKeys.color3:
                toReturn.Add("bounce",sound_3_bounce);
                toReturn.Add("fuse",sound_3_fuse);
                break;
            case identityKeys.color4:
                toReturn.Add("bounce",sound_4_bounce);
                toReturn.Add("fuse",sound_4_fuse);
                break;
            case identityKeys.color5:
                toReturn.Add("bounce",sound_5_bounce);
                toReturn.Add("fuse",sound_5_fuse);
                break;
            case identityKeys.color6:
                toReturn.Add("bounce",sound_6_bounce);
                toReturn.Add("fuse",sound_6_fuse);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(identityKeys), identityKeys, null);
        }

        return toReturn;
    }
}
