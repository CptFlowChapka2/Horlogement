using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class DataHolder : MonoBehaviour
{

    [SerializeField] private Color color0 ;
    [SerializeField] private Color color1 ;
    [SerializeField] private Color color2 ;
    
    [SerializeField] private AudioClip sound0 ;
    [SerializeField] private AudioClip sound1 ;
    [SerializeField] private AudioClip sound2 ;

    public List<Color> allColor = new List<Color>();
    public Dictionary<Color, Dictionary<string,object>> entityIdentity = new Dictionary<Color, Dictionary<string,object>>();

    private void Start()
    {
        CreateColorList();
        CreateDictionary();
    }

    private void CreateDictionary()
    {
        Vector3 baseVector = new Vector3(1, 0, 0);
        
        Dictionary<string, object> identity0 = new Dictionary<string, object>();
        identity0.Add("color",color0);
        identity0.Add("Vector",baseVector);
       // identity0.Add("sound",sound0);
        entityIdentity.Add(color0,identity0);
        
        Dictionary<string, object> identity1 = new Dictionary<string, object>();
        identity1.Add("color",color1);
        identity1.Add("Vector",Quaternion.AngleAxis(51.42f, Vector3.up)*baseVector);
        //identity0.Add("sound",sound1);
        entityIdentity.Add(color1,identity1);
        
        Dictionary<string, object> identity2 = new Dictionary<string, object>();
        identity2.Add("color",color2);
        identity2.Add("Vector",(Quaternion.AngleAxis(102.84f, Vector3.up)*baseVector));
        //identity0.Add("sound",sound2);
        entityIdentity.Add(color2,identity2);
        
        

    }

    private void CreateColorList()
    {
        allColor.Add(color0);
        allColor.Add(color1);
        allColor.Add(color2);
    }
}
