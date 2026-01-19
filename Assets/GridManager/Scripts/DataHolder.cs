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
    public SoundHandler soundHandler;

    [Space] [Header("Boreal")] [Space] public int startingNbr=2;
    
    [Space][Header("WallFeedback")][Space]
    [SerializeField]public AudioClip wallCreate2;
    [SerializeField]public AudioClip wallCreate1;
    public Material willDestroylWall;
    public Material normallWall;
    public Material illegalWall;
    public Material phantomlWall;
    
    
    
    [Space][Header("identity 0")][Space]
    [SerializeField] private Material color_main_0 ;
    [SerializeField] private Material color_trail_0 ;
    [SerializeField] private AudioClip sound_0_bounce ;
    [SerializeField] private AudioClip sound_0_fuse ;
    public Material borealLowFeedBackMatt0;
    public Material borealHighFeedBackMatt0;
    public Material borealOffFeedBackMatt0;
    
    [Space][Header("identity 1")][Space]
    [SerializeField] private Material color_main_1 ;
    [SerializeField] private Material color_trail_1 ;
    [SerializeField] private AudioClip sound_1_bounce ;
    [SerializeField] private AudioClip sound_1_fuse ;
    public Material borealLowFeedBackMatt1;
    public Material borealHighFeedBackMatt1;
    public Material borealOffFeedBackMatt1;
   
    [Space][Header("identity 2")][Space]
    [SerializeField] private Material color_main_2 ;
    [SerializeField] private Material color_trail_2 ;
    
    [SerializeField] private AudioClip sound_2_bounce ;
    [SerializeField] private AudioClip sound_2_fuse ;
    
    public Material borealLowFeedBackMatt2;
    public Material borealHighFeedBackMatt2;
    public Material borealOffFeedBackMatt2;
    [Space][Header("identity 3")][Space]
    [SerializeField] private Material color_main_3 ;
    [SerializeField] private Material color_trail_3 ;
    [SerializeField] private AudioClip sound_3_bounce ;
    [SerializeField] private AudioClip sound_3_fuse ;
    
    public Material borealLowFeedBackMatt3;
    public Material borealHighFeedBackMatt3;
    public Material borealOffFeedBackMatt3;
    
    [Space][Header("identity 4")][Space]
    [SerializeField] private Material color_main_4 ;
    [SerializeField] private Material color_trail_4 ;
    [SerializeField] private AudioClip sound_4_bounce ;
    [SerializeField] private AudioClip sound_4_fuse ;
    
    public Material borealLowFeedBackMatt4;
    public Material borealHighFeedBackMatt4;
    public Material borealOffFeedBackMatt4;
    
    [Space][Header("identity 5")][Space]

    [SerializeField] private Material color_main_5 ;
    [SerializeField] private Material color_trail_5 ;
    [SerializeField] private AudioClip sound_5_bounce ;
    [SerializeField] private AudioClip sound_5_fuse ;
    
    public Material borealLowFeedBackMatt5;
    public Material borealHighFeedBackMatt5;
    public Material borealOffFeedBackMatt5;
    
    [Space][Header("identity 6")][Space]
    [SerializeField] private Material color_main_6 ;
    [SerializeField] private Material color_trail_6 ;
    [SerializeField] private AudioClip sound_6_bounce ;
    [SerializeField] private AudioClip sound_6_fuse ;

    public Material borealLowFeedBackMatt6;
    public Material borealHighFeedBackMatt6;
    public Material borealOffFeedBackMatt6;
    
    
    public float speed =1;
    public float addedAngle =51.42f;
    
    
    public Dictionary<identityKeys, Dictionary<string,object>> entityIdentity = new Dictionary<identityKeys, Dictionary<string,object>>();
    

    private void Awake()
    {
        CreateDictionary();
    }

    private void CreateDictionary()
    {
        Vector3 baseVector = new Vector3(1, 0, 0);
        
        Dictionary<string, object> identity0 = new Dictionary<string, object>
        {
            { "color", CreateColorDico(identityKeys.color0) },
            { "Vector", baseVector },
            {"sound",CreateSoundDico(identityKeys.color0)}

        };
        
        entityIdentity.Add(identityKeys.color0,identity0);
        
        Dictionary<string, object> identity1 = new Dictionary<string, object>
        {
            { "color", CreateColorDico(identityKeys.color1) },
            { "Vector", Quaternion.AngleAxis(addedAngle, Vector3.up) * baseVector },
            {"sound",CreateSoundDico(identityKeys.color1)}
            
        };
        
        entityIdentity.Add(identityKeys.color1,identity1);
        
        Dictionary<string, object> identity2 = new Dictionary<string, object>
        {
            { "color", CreateColorDico(identityKeys.color2) },
            { "Vector", (Quaternion.AngleAxis(addedAngle*2, Vector3.up) * baseVector) },
            {"sound",CreateSoundDico(identityKeys.color2)}

        };
        
        entityIdentity.Add(identityKeys.color2,identity2);
        Dictionary<string, object> identity3 = new Dictionary<string, object>
        {
            { "color", CreateColorDico(identityKeys.color3) },
            { "Vector", (Quaternion.AngleAxis(addedAngle*3, Vector3.up) * baseVector) },
            {"sound",CreateSoundDico(identityKeys.color3)}

        };
        
        entityIdentity.Add(identityKeys.color3,identity3);
        Dictionary<string, object> identity4 = new Dictionary<string, object>
        {
            { "color", CreateColorDico(identityKeys.color4) },
            { "Vector", (Quaternion.AngleAxis(addedAngle*4, Vector3.up) * baseVector) },
            {"sound",CreateSoundDico(identityKeys.color4)}

        };
        
        entityIdentity.Add(identityKeys.color4,identity4);
        Dictionary<string, object> identity5 = new Dictionary<string, object>
        {
            { "color", CreateColorDico(identityKeys.color5) },
            { "Vector", (Quaternion.AngleAxis(addedAngle*5, Vector3.up) * baseVector) },
            {"sound",CreateSoundDico(identityKeys.color5)}

        };
        
        entityIdentity.Add(identityKeys.color5,identity5);
        
        Dictionary<string, object> identity6 = new Dictionary<string, object>
        {
            { "color", CreateColorDico(identityKeys.color6) },
            { "Vector", (Quaternion.AngleAxis(addedAngle*6, Vector3.up) * baseVector) },
            {"sound",CreateSoundDico(identityKeys.color6)}
        };
        
        entityIdentity.Add(identityKeys.color6,identity6);

    }
    

    private Dictionary<string,AudioClip> CreateSoundDico(identityKeys identityKeys)
    {
        Dictionary<string, AudioClip> toReturn = new Dictionary<string, AudioClip>();
        switch (identityKeys)
        {

            case identityKeys.notAsignated:
                throw new InvalidDataException();
                
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
    private Dictionary<string,Material> CreateColorDico(identityKeys identityKeys)
    {
        Dictionary<string, Material> toReturn = new Dictionary<string, Material>();
        switch (identityKeys)
        {

            case identityKeys.notAsignated:
                throw new InvalidDataException();
                
            case identityKeys.color0:
                toReturn.Add("main",color_main_0);
                toReturn.Add("trail",color_trail_0);
                break;
            case identityKeys.color1:
                toReturn.Add("main",color_main_1);
                toReturn.Add("trail",color_trail_1);
                break;
            case identityKeys.color2:
                toReturn.Add("main",color_main_2);
                toReturn.Add("trail",color_trail_2);
                break;
            case identityKeys.color3:
                toReturn.Add("main",color_main_3);
                toReturn.Add("trail",color_trail_3);
                break;
            case identityKeys.color4:
                toReturn.Add("main",color_main_4);
                toReturn.Add("trail",color_trail_4);
                break;
            case identityKeys.color5:
                toReturn.Add("main",color_main_5);
                toReturn.Add("trail",color_trail_5);
                break;
            case identityKeys.color6:
                toReturn.Add("main",color_main_6);
                toReturn.Add("trail",color_trail_6);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(identityKeys), identityKeys, null);
        }

        return toReturn;
    }
}
