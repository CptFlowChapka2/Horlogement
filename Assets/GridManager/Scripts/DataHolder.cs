using System;
using System.Collections.Generic;

using System.IO;
using UnityEngine;

public enum identityKeys
{
    notAsignated,
    A,
    B,
    C,
    D,
    E,
    F,
    G,
}
public class DataHolder : MonoBehaviour
{
    public GameObject intantiateDummy;
    public SoundHandler soundHandler;

    [Space] [Header("Cursor")] [Space] [SerializeField]
    public Texture2D cursorNull;
    [SerializeField]public Texture2D cursorDestroyWall;
    [SerializeField]public Texture2D cursorPhantom;
    [SerializeField]public Texture2D cursorBoreal0;
    [SerializeField]public Texture2D cursorBoreal1;
    [SerializeField]public Texture2D cursorBoreal2;
    [SerializeField]public Texture2D cursorBoreal3;
    [SerializeField]public Texture2D cursorBoreal4;
    [SerializeField]public Texture2D cursorBoreal5;
    [SerializeField]public Texture2D cursorBoreal6;
    
    
    
    [Space] [Header("Boreal")] [Space] public int startingNbr=2;
    
    [Space][Header("WallFeedback")][Space]
    [SerializeField]public AudioClip wallCreate2;
    [SerializeField]public AudioClip wallCreate1;
    public Material willDestroylWall;
    public Material willDestroylWall2;
    public Material normallWall;
    public Material normallWall2;
    public Material illegalWall;
    public Material illegalWall2;
    public Material phantomlWall;
    public Material phantomlWall2;


    public AudioClip onVoided;
    
    [Space][Header("identity 0")][Space]
    [SerializeField] private Material color_main_0 ;
    [SerializeField] private Material color_trail_0 ;
    [SerializeField] private AudioClip sound_0_bounce ;
    [SerializeField] private AudioClip sound_0_fuse ;
    public Material borealLowFeedBackMatt0;
    public Material borealOverLowFeedBackMatt0;
    public Material borealHighFeedBackMatt0;
    public Material borealOverHighFeedBackMatt0;
    public Material borealOffFeedBackMatt0;
    
    
    [Space][Header("identity 1")][Space]
    [SerializeField] private Material color_main_1 ;
    [SerializeField] private Material color_trail_1 ;
    [SerializeField] private AudioClip sound_1_bounce ;
    [SerializeField] private AudioClip sound_1_fuse ;
    public Material borealLowFeedBackMatt1;
    public Material borealOverLowFeedBackMatt1;
    public Material borealHighFeedBackMatt1;
    public Material borealOverHighFeedBackMatt1;
    public Material borealOffFeedBackMatt1;
    
   
    [Space][Header("identity 2")][Space]
    [SerializeField] private Material color_main_2 ;
    [SerializeField] private Material color_trail_2 ;
    
    [SerializeField] private AudioClip sound_2_bounce ;
    [SerializeField] private AudioClip sound_2_fuse ;
    
    public Material borealLowFeedBackMatt2;
    public Material borealOverLowFeedBackMatt2;
    public Material borealHighFeedBackMatt2;
    public Material borealOverHighFeedBackMatt2;
    public Material borealOffFeedBackMatt2;
    
    [Space][Header("identity 3")][Space]
    [SerializeField] private Material color_main_3 ;
    [SerializeField] private Material color_trail_3 ;
    [SerializeField] private AudioClip sound_3_bounce ;
    [SerializeField] private AudioClip sound_3_fuse ;
    
    public Material borealLowFeedBackMatt3;
    public Material borealOverLowFeedBackMatt3;
    public Material borealHighFeedBackMatt3;
    public Material borealOverHighFeedBackMatt3;
    public Material borealOffFeedBackMatt3;
   
    
    [Space][Header("identity 4")][Space]
    [SerializeField] private Material color_main_4 ;
    [SerializeField] private Material color_trail_4 ;
    [SerializeField] private AudioClip sound_4_bounce ;
    [SerializeField] private AudioClip sound_4_fuse ;
    
    public Material borealLowFeedBackMatt4;
    public Material borealOverLowFeedBackMatt4;
    public Material borealHighFeedBackMatt4;
    public Material borealOverHighFeedBackMatt4;
    public Material borealOffFeedBackMatt4;
    
    
    [Space][Header("identity 5")][Space]

    [SerializeField] private Material color_main_5 ;
    [SerializeField] private Material color_trail_5 ;
    [SerializeField] private AudioClip sound_5_bounce ;
    [SerializeField] private AudioClip sound_5_fuse ;
    
    public Material borealLowFeedBackMatt5;
    public Material borealOverLowFeedBackMatt5;
    public Material borealHighFeedBackMatt5;
    public Material borealOverHighFeedBackMatt5;
    public Material borealOffFeedBackMatt5;
    
    
    [Space][Header("identity 6")][Space]
    [SerializeField] private Material color_main_6 ;
    [SerializeField] private Material color_trail_6 ;
    [SerializeField] private AudioClip sound_6_bounce ;
    [SerializeField] private AudioClip sound_6_fuse ;

    public Material borealLowFeedBackMatt6;
    public Material borealOverLowFeedBackMatt6;
    public Material borealHighFeedBackMatt6;
    public Material borealOverHighFeedBackMatt6;
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
            { "color", CreateColorDico(identityKeys.A) },
            { "Vector", baseVector },
            {"sound",CreateSoundDico(identityKeys.A)}

        };
        
        entityIdentity.Add(identityKeys.A,identity0);
        
        Dictionary<string, object> identity1 = new Dictionary<string, object>
        {
            { "color", CreateColorDico(identityKeys.B) },
            { "Vector", Quaternion.AngleAxis(addedAngle, Vector3.up) * baseVector },
            {"sound",CreateSoundDico(identityKeys.B)}
            
        };
        
        entityIdentity.Add(identityKeys.B,identity1);
        
        Dictionary<string, object> identity2 = new Dictionary<string, object>
        {
            { "color", CreateColorDico(identityKeys.C) },
            { "Vector", (Quaternion.AngleAxis(addedAngle*2, Vector3.up) * baseVector) },
            {"sound",CreateSoundDico(identityKeys.C)}

        };
        
        entityIdentity.Add(identityKeys.C,identity2);
        Dictionary<string, object> identity3 = new Dictionary<string, object>
        {
            { "color", CreateColorDico(identityKeys.D) },
            { "Vector", (Quaternion.AngleAxis(addedAngle*3, Vector3.up) * baseVector) },
            {"sound",CreateSoundDico(identityKeys.D)}

        };
        
        entityIdentity.Add(identityKeys.D,identity3);
        Dictionary<string, object> identity4 = new Dictionary<string, object>
        {
            { "color", CreateColorDico(identityKeys.E) },
            { "Vector", (Quaternion.AngleAxis(addedAngle*4, Vector3.up) * baseVector) },
            {"sound",CreateSoundDico(identityKeys.E)}

        };
        
        entityIdentity.Add(identityKeys.E,identity4);
        Dictionary<string, object> identity5 = new Dictionary<string, object>
        {
            { "color", CreateColorDico(identityKeys.F) },
            { "Vector", (Quaternion.AngleAxis(addedAngle*5, Vector3.up) * baseVector) },
            {"sound",CreateSoundDico(identityKeys.F)}

        };
        
        entityIdentity.Add(identityKeys.F,identity5);
        
        Dictionary<string, object> identity6 = new Dictionary<string, object>
        {
            { "color", CreateColorDico(identityKeys.G) },
            { "Vector", (Quaternion.AngleAxis(addedAngle*6, Vector3.up) * baseVector) },
            {"sound",CreateSoundDico(identityKeys.G)}
        };
        
        entityIdentity.Add(identityKeys.G,identity6);

    }
    

    private Dictionary<string,AudioClip> CreateSoundDico(identityKeys identityKeys)
    {
        Dictionary<string, AudioClip> toReturn = new Dictionary<string, AudioClip>();
        switch (identityKeys)
        {

            case identityKeys.notAsignated:
                throw new InvalidDataException();
                
            case identityKeys.A:
                toReturn.Add("bounce",sound_0_bounce);
                toReturn.Add("fuse",sound_0_fuse);
                break;
            case identityKeys.B:
                toReturn.Add("bounce",sound_1_bounce);
                toReturn.Add("fuse",sound_1_fuse);
                break;
            case identityKeys.C:
                toReturn.Add("bounce",sound_2_bounce);
                toReturn.Add("fuse",sound_2_fuse);
                break;
            case identityKeys.D:
                toReturn.Add("bounce",sound_3_bounce);
                toReturn.Add("fuse",sound_3_fuse);
                break;
            case identityKeys.E:
                toReturn.Add("bounce",sound_4_bounce);
                toReturn.Add("fuse",sound_4_fuse);
                break;
            case identityKeys.F:
                toReturn.Add("bounce",sound_5_bounce);
                toReturn.Add("fuse",sound_5_fuse);
                break;
            case identityKeys.G:
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
                
            case identityKeys.A:
                toReturn.Add("main",color_main_0);
                toReturn.Add("trail",color_trail_0);
                break;
            case identityKeys.B:
                toReturn.Add("main",color_main_1);
                toReturn.Add("trail",color_trail_1);
                break;
            case identityKeys.C:
                toReturn.Add("main",color_main_2);
                toReturn.Add("trail",color_trail_2);
                break;
            case identityKeys.D:
                toReturn.Add("main",color_main_3);
                toReturn.Add("trail",color_trail_3);
                break;
            case identityKeys.E:
                toReturn.Add("main",color_main_4);
                toReturn.Add("trail",color_trail_4);
                break;
            case identityKeys.F:
                toReturn.Add("main",color_main_5);
                toReturn.Add("trail",color_trail_5);
                break;
            case identityKeys.G:
                toReturn.Add("main",color_main_6);
                toReturn.Add("trail",color_trail_6);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(identityKeys), identityKeys, null);
        }

        return toReturn;
    }
}
